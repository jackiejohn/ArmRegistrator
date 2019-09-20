using System;
using System.Diagnostics;
using System.Threading;
using ConcurrenceType;
using RadioModule;
using SharedTypes.Paks;

namespace ArmRegistrator.Radio
{
    public class RModuleWrapper:IDisposable
    {

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
                    StopCommunication();
                    if (_rModule != null)
                    {
                        _rModule.Dispose();
                        _rModule = null;
                    }
                }
            }
            _isDisposed = true;
        }
        public void Close()
        {
            StopCommunication();
            if (_rModule != null)
            {
                _rModule.Dispose();
                _rModule = null;
            }
        }



        public event EventHandler OnDataReceived;

        public delegate void SerialPortErrorHandler(object sender, EventArgs e);
        public event SerialPortErrorHandler OnPortError;

        public bool TryConnect(string portName, string baudRate)
        {
            if (_rModule == null)
            {
                var config = Configuration.GetDefault();
                _rModule = new RModule(portName, config) { BaudRate = Convert.ToInt32(baudRate) };
            }
            if (_rModule.IsInit)
            {
                IsConnected = true;
                return true;
            }
            
            var initResult = _rModule.InitRModule();
            lock (_lockerInitError)
            {
                _initError = initResult;
            }
            if (!string.IsNullOrEmpty(initResult))
            {
                _rModule = null;
                IsConnected = false;
                return false;
            }
            _rModule.SetDefaultParser();
            _rModule.OnPortError += RModule_OnPortError;
            _rModule.OnDataReceived += RModule_OnDataReceived;
            IsConnected = true;
            return true;
        }

        public void StartCommunication()
        {
            if (_rModule != null)
            {
                _rModule.StartCommunication();
                IsConnected=true;
            }
        }
        public void StopCommunication()
        {
            if(_rModule!=null)_rModule.StopCommunication();
            IsConnected = false;
        }


        public bool ObjectGetStatus(out UInt16 status, int objectId)
        {
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.RequestStatus, TimeSpan.FromMilliseconds(500));
        }
        public bool ObjectSendPassiveMode(out UInt16 status, int objectId)
        {
            if (!IsConnected)
            {
                status = default(UInt16);
                return false;
            }
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.SetModePassive, TimeSpan.FromSeconds(1));
        }
        public bool ObjectSendWorkMode(out UInt16 status, int objectId)
        {
            if (!IsConnected)
            {
                status = default(UInt16);
                return false;
            }
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.SetModeWork, TimeSpan.FromSeconds(3));
        }


        public string InitError
        {
            get{
                lock (_lockerInitError)
                {
                    return _initError;
                }}
        }
        public bool IsConnected
        {
            get
            {
                lock (_lockerInitError)
                {
                    return _isConnected;
                }
            }
            private set
            {
                lock (_lockerIsConnected)
                {
                    _isConnected = value;
                }
            }

        }

        private bool SendCommandToObject(out UInt16 status, UInt16 adr, PakCommands command, TimeSpan waitTime)
        {
            status = default(UInt16);
            var pakReqCoord = new PakStructRequest();


            // Пакет запроса координат
            const ushort pakNumber = UInt16.MaxValue;
            pakReqCoord.SetRequestCoordinate(adr, pakNumber, command, 0, 0);
            pakReqCoord.ActualDateTime();
            pakReqCoord.WriteCrc16();
            var tpReq = new ModulePak(pakReqCoord, false);
            //_mreWaitData.Reset();
            var queue = (CQueue<ModulePak>)_rModule.InQueue;
            queue.DequeueAll();
            _mreHaveData.Reset();
            _rModule.OutQueueEnq(tpReq);
            bool sign = _mreHaveData.WaitOne(TimeSpan.FromSeconds(10));
            //bool sign = _mreHaveData.WaitOne(10););)
            if (!sign) return false; //Не дождались ответа от трекера
            ModulePak pak;
            int i;
            for (i = 0; i < 2; ++i)//for (int i = 0; i < 2; i++)
            {
                while (queue.TryDequeue(out pak))
                {
                    var ps = pak.Structure as PakStructAnsw1;
                    if (ps != null)
                    {
                        if (ps.Address == adr && ps.IsCrcOk())
                        {
                            status = ps.Status;
                            return true;
                        }
                    }
                }
                Thread.Sleep(waitTime); // Ждем, что пройдут тесты
                //Thread.Sleep(TimeSpan.FromSeconds(1)); 
            }
            Debug.WriteLine("RetryCount " + i);
            return false;
        }

        void RModule_OnDataReceived(object sender, EventArgs e)
        {
            _mreHaveData.Set();
            InvokeOnDataReceived(e);
        }

        void RModule_OnPortError(object sender, EventArgs e)
        {
            InvokeOnPortError(e);
        }

        private void InvokeOnPortError(EventArgs e)
        {
            SerialPortErrorHandler handler = OnPortError;
            if (handler != null) handler(this, e);
        }

        private void InvokeOnDataReceived(EventArgs e)
        {
            EventHandler handler = OnDataReceived;
            if (handler != null) handler(this, e);
        }

        private RModule _rModule;
        private string _initError;
        private readonly object _lockerInitError=new object();
        private readonly object _lockerIsConnected = new object();

        private bool _isDisposed;

        private readonly ManualResetEvent _mreHaveData = new ManualResetEvent(true);

        private bool _isConnected;

    }
}
