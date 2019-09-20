using System;
using System.IO.Ports;
using ArmRegistrator.DataBase;
using ArmRegistrator.Radio;
using RfidLib;
using SharedTypes.Paks;

namespace ArmRegistrator.Automation
{
    public class Logic
    {
        public Logic(string portIn, string portOut, DbWrapper dbWrapper, RModuleWrapper rmWrapper)
            : this(portIn, dbWrapper, rmWrapper)
        {
            _readerOut=new SerialReader(new SerialPort(portOut));
            _readerOut.OnSerialStringReaded += ReaderOut_OnSerialStringReaded;
            _twoReaders = true;
        }
        public Logic(string port, DbWrapper dbWrapper, RModuleWrapper rmWrapper)
        {
            _readerIn = new SerialReader(new SerialPort(port));
            _readerIn.OnSerialStringReaded += ReaderIn_OnSerialStringReaded;
            _dbWrapper = dbWrapper;
            _rmWrapper = rmWrapper;
        }

        public bool Start()
        {
            _readerIn.StartReading();
            if (_readerIn.Error != SerialReaderErrorEnum.None)
            {
                _readerIn.StopReading();
                return false;
            }
            if (_twoReaders)
            {
                _readerOut.StartReading();
                if (_readerOut.Error != SerialReaderErrorEnum.None)
                {
                    _readerIn.StopReading();
                    _readerOut.StopReading();
                    return false;
                }
            }
            IsStarted = true;
            return true;
        }
        public void Stop()
        {
            _readerIn.StopReading();
            IsStarted = false;
            if (_twoReaders) _readerOut.StopReading();
        }

        public bool IsStarted
        {
            get{
                lock (_lockerIsStarted)
                {
                    return _isStarted;
                }}
            private set
            {
                lock (_lockerIsStarted)
                {
                    _isStarted=value;
                }
            }

        }


        public event EventHandler OnObjectToWork;
        public event EventHandler OnObjectToLamp;

        
        private DbWrapper.ObjectRecordValue GetObjectRecValue(string rfidLabel)
        {
            if (string.IsNullOrEmpty(rfidLabel)) return new DbWrapper.ObjectRecordValue();
            rfidLabel = rfidLabel.Replace("\r", "");

            // 1. Проверка в БД на наличие
            return _dbWrapper.ReadObjectId(rfidLabel);
        }

        void ReaderOut_OnSerialStringReaded(object sender, SerialReaderEventArgs e)
        {
            // 1. Проверка в БД на наличие
            var objectState = GetObjectRecValue(e.ReadedString);
            if (objectState.ObjectId == 0) return;

            // 2. Контроль логики "выход" 
            if (objectState.InField ) return;

            // 3. Посылка команды в эфир
            bool isChanged = true;
            UInt16 status;
            isChanged = _rmWrapper.ObjectSendWorkMode(out status, objectState.ActiveObjectId);
            isChanged = isChanged && !PakStatusWord.Instance(status).LampMode;
            

            // 4. Запись в БД
            /*if (isChanged)*/
            var isWrited = _dbWrapper.WriteObjectInFieldState(true, objectState.ObjectId, false);


            // 3. Позиционирование и обновление данных на экране в списке
            if (isWrited)
            {
                _dbWrapper.RefreshDataSetAllTables();
                InvokeOnObjectToWork(new EventArgs());
                //TrackerViewRowChanged();
            }
        }

        void ReaderIn_OnSerialStringReaded(object sender, SerialReaderEventArgs e)
        {
            // 1. Проверка в БД на наличие
            var objectState = GetObjectRecValue(e.ReadedString);
            if (objectState.ObjectId==0) return;

            // 2. Контроль логики "вход-выход" или только "вход" 
            if (!objectState.InField && _twoReaders) return;

            // 3. Посылка команды в эфир
            bool isChanged = true;
            UInt16 status;
            if (objectState.InField)
            {
                isChanged = _rmWrapper.ObjectSendPassiveMode(out status, objectState.ActiveObjectId);
            }
            else
            {
                isChanged = _rmWrapper.ObjectSendWorkMode(out status, objectState.ActiveObjectId);
                isChanged = isChanged && !PakStatusWord.Instance(status).LampMode;
            }
            
            // 4. Запись в БД
            /*if (isChanged)*/ 
            var isWrited= _dbWrapper.WriteObjectInFieldState(true, objectState.ObjectId, objectState.InField);

           
            // 3. Позиционирование и обновление данных на экране в списке
            if (isWrited)
            {
                _dbWrapper.RefreshDataSetAllTables();
                if (objectState.InField)
                {
                    InvokeOnObjectToLamp(new EventArgs());
                }
                else
                {
                    InvokeOnObjectToWork(new EventArgs());
                    
                }
                //TrackerViewRowChanged();
            }
        }

        private void InvokeOnObjectToWork(EventArgs e)
        {
            EventHandler handler = OnObjectToWork;
            if (handler != null) handler(this, e);
        }

        private void InvokeOnObjectToLamp(EventArgs e)
        {
            EventHandler handler = OnObjectToLamp;
            if (handler != null) handler(this, e);
        }

        



        private readonly SerialReader _readerIn;
        private readonly SerialReader _readerOut;
        private readonly bool _twoReaders;
        private readonly DbWrapper _dbWrapper;
        private readonly RModuleWrapper _rmWrapper;

        private bool _isStarted;

        private readonly object _lockerIsStarted=new object();
    }
}
