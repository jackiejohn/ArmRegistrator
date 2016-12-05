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
        public ChoisePortDlg(string portName):this()
        {
            FieldPort.Text = portName;
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
            FieldPort.Items.Clear();
            if (fieldPorts.Length != 0)
            {
                FieldPort.Items.AddRange(fieldPorts);
                FieldPort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(@"В системе не найдены последовательные порты!", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
