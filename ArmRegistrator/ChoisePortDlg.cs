using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ArmRegistrator
{
    public partial class ChoisePortDlg : Form
    {
        public ChoisePortDlg()
        {
            InitializeComponent();
        }
        public ChoisePortDlg(string portName, string baudRate):this()
        {
            FieldPort.Text = portName;
            FieldBaudRate.Text = baudRate;
        }

        private void FieldPort_Enter(object sender, EventArgs e)
        {
            string[] fieldPorts = SerialPort.GetPortNames();
            if (!fieldPorts.Equals(FieldPort.Items))
            {
                FieldPort.Items.Clear();
                if (fieldPorts.Length != 0)
                {
                    FieldPort.Items.AddRange(fieldPorts);
                    FieldPort.SelectedIndex = 0;
                }
            }
        }

        private void ChoisePortDlg_Load(object sender, EventArgs e)
        {
            // Com-порт
            string[] fieldPorts = SerialPort.GetPortNames();
            string[] fieldBaudRates = new[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            FieldPort.Items.Clear();
            if (fieldPorts.Length != 0)
            {
                FieldPort.Items.AddRange(fieldPorts);
                if (string.IsNullOrEmpty(FieldPort.Text)) FieldPort.SelectedIndex = 0;
                FieldBaudRate.Items.AddRange(fieldBaudRates);
                if (string.IsNullOrEmpty(FieldBaudRate.Text)) FieldBaudRate.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(@"В системе не найдены последовательные порты!", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FieldPort.Text) || string.IsNullOrEmpty(FieldBaudRate.Text))
            {
                MessageBox.Show("Не указан порт или скорость!", "Информация", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
