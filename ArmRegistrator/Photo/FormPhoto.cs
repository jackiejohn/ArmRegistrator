using System;
using System.Windows.Forms;
using ArmRegistrator.DataBase;
using Timer = System.Timers.Timer;

namespace ArmRegistrator.Photo
{
    public partial class FormPhoto : Form
    {
        public FormPhoto()
        {
            InitializeComponent();
            _timerInterval = (int) TimeSpan.FromSeconds(30).TotalMilliseconds;
            _timerIn= new Timer()
                         {
                             Interval = _timerInterval
                         };
            _timerIn.Elapsed += TimerIn_Tick;
            _timerIn.Start();
            //_timerIn.Enabled = false;

            _timerOut = new Timer
                            {
                                Interval = _timerInterval
                            };
            
            _timerOut.Elapsed += TimerOut_Tick;
            _timerOut.Start();
            //_timerOut.Enabled = false;
        }

        void TimerOut_Tick(object sender, EventArgs e)
        {
            UpdatePersonalData(new DbWrapper.PersonalData() {Photo = Properties.Resources.NoEmployeeImage2}, false,false);
        }

        void TimerIn_Tick(object sender, EventArgs e)
        {
            UpdatePersonalData(new DbWrapper.PersonalData() { Photo = Properties.Resources.NoEmployeeImage2 }, true, false);
        }

        public void Maximized()
        {
            WindowState = FormWindowState.Maximized;
            FormPhotoHelper.ScaleControls(Controls,Size);
            
        }
        public void Standartized()
        {
            WindowState = FormWindowState.Normal;
            FormPhotoHelper.SetDefaultState(this);
            
        }
        public void UpdatePersonalData(DbWrapper.PersonalData data, bool inData, bool enableTimer)
        {
            if (inData)
            {
                _timerIn.Enabled = false;
                FormHelper.InvokeLableSetText(lblInFio, data.Fio);
                FormHelper.InvokeLableSetText(lblInDolj, data.Dolj);
                FormHelper.InvokeLableSetText(lblInId, data.ObjectId.ToString());
                FormHelper.InvokePictureBoxSetImage(PhotoIn,data.Photo);
                if (enableTimer)
                {
                    _timerIn.Interval = _timerInterval;
                    _timerIn.Enabled = true;
                }
            }
            else
            {
                _timerOut.Enabled = false;
                FormHelper.InvokeLableSetText(lblOutFio, data.Fio);
                FormHelper.InvokeLableSetText(lblOutDolj, data.Dolj);
                FormHelper.InvokeLableSetText(lblOutId, data.ObjectId.ToString());
                FormHelper.InvokePictureBoxSetImage(PhotoOut, data.Photo);
                if (enableTimer)
                {
                    _timerOut.Interval = _timerInterval;
                    _timerOut.Enabled = true;
                }
                
            }
        }

        private readonly Timer _timerIn;
        private readonly Timer _timerOut;
        private readonly int _timerInterval;
    }
}
