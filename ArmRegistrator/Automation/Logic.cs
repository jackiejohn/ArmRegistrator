using System;
using System.IO.Ports;
using System.Threading;
using ArmRegistrator.DataBase;
using ArmRegistrator.Radio;
using RfidLib;
using SharedTypes.Paks;

namespace ArmRegistrator.Automation
{
    public class Logic:IDisposable
    {
        public Logic(string portIn, string portOut, DbWrapper dbWrapper, RModuleWrapper rmWrapper)
            : this(portIn, dbWrapper, rmWrapper)
        {
            _readerOut=new SerialReader(new SerialPort(portOut){ReadTimeout = 5000});
            _readerOut.OnSerialStringReaded += ReaderOut_OnSerialStringReaded;
            _twoReaders = true;
        }
        public Logic(string port, DbWrapper dbWrapper, RModuleWrapper rmWrapper)
        {
            _readerIn = new SerialReader(new SerialPort(port){ReadTimeout = 5000});
            _readerIn.OnSerialStringReaded += ReaderIn_OnSerialStringReaded;
            _dbWrapper = dbWrapper;
            _rmWrapper = rmWrapper;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    //Stop();
                    IsStarted = false;
                    if (_readerIn != null)
                    {
                        _readerIn.Dispose();
                        _readerIn = null;
                    }
                    if (_readerOut != null)
                    {
                        _readerOut.Dispose();
                        _readerOut = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public bool Start()
        {
            var waitStarting = (int)TimeSpan.FromSeconds(10).TotalMilliseconds;
            var mre = new ManualResetEvent(false);
            //_readerIn.StartReading();
            _readerIn.StartReading(new[]{mre});
            mre.WaitOne(waitStarting);
            if (_readerIn.Error == SerialReaderErrorEnum.PortOpenError)
            {
                _error |= (int)LogicErrorEnum.PortInOpenError;
                _readerIn.StopReading();
                mre.Close();
                return false;
            }
            
            if (_twoReaders)
            {
                var mreOut = new ManualResetEvent(false);
                _readerOut.StartReading(new[] { mreOut });
                mreOut.WaitOne(waitStarting);
                mreOut.Close();
                if (_readerOut.Error == SerialReaderErrorEnum.PortOpenError)
                {
                    _error |= (int)LogicErrorEnum.PortOutOpenError;
                    _readerIn.StopReading();
                    mre.Close();
                    _readerOut.StopReading();
                    mreOut.Close();
                    return false;
                }
                mreOut.Close();
            }
            mre.Close();
            _error = (int)LogicErrorEnum.None;
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

        public delegate void ObjectChangeStateHandler(object sender, ObjectChangeStateEventArgs e);
        public event ObjectChangeStateHandler OnObjectToWork;
        public event ObjectChangeStateHandler OnObjectToLamp;

        
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
                InvokeOnObjectToWork(new ObjectChangeStateEventArgs(objectState.ObjectId));
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
                    InvokeOnObjectToLamp(new ObjectChangeStateEventArgs(objectState.ObjectId));
                }
                else
                {
                    InvokeOnObjectToWork(new ObjectChangeStateEventArgs(objectState.ObjectId));
                }
                //TrackerViewRowChanged();
            }
        }

        public void InvokeOnObjectToWork(ObjectChangeStateEventArgs e)
        {
            ObjectChangeStateHandler handler = OnObjectToWork;
            if (handler != null) handler(this, e);
        }

        public void InvokeOnObjectToLamp(ObjectChangeStateEventArgs e)
        {
            ObjectChangeStateHandler handler = OnObjectToLamp;
            if (handler != null) handler(this, e);
        }

        public LogicErrorEnum Error { get { return (LogicErrorEnum)_error; } }



        private SerialReader _readerIn;
        private SerialReader _readerOut;
        private readonly bool _twoReaders;
        private readonly DbWrapper _dbWrapper;
        private readonly RModuleWrapper _rmWrapper;

        private bool _isStarted;

        private readonly object _lockerIsStarted=new object();

        private int _error;
        private bool _isDisposed;
    }
}
