using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ArmRegistrator
{
    public partial class FormSerialSettings : Form
    {
        public FormSerialSettings()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void RetriveData()
        {
            cbPortIn.Text = Properties.Settings.Default.RfidPortIn;
            cbPortOut.Text = Properties.Settings.Default.RfidPortOut;
            cbPortRModem.Text = Properties.Settings.Default.RModemPort;
            cbBaudRateRModem.Text = Properties.Settings.Default.RModemBaudRate;
            FlgTwoReaders.Checked = Properties.Settings.Default.RfidTwoReaders;
        }
        private void SaveData()
        {
            Properties.Settings.Default.RfidPortIn = cbPortIn.Text;
            Properties.Settings.Default.RfidPortOut = cbPortOut.Text;
            Properties.Settings.Default.RModemPort = cbPortRModem.Text;
            Properties.Settings.Default.RModemBaudRate = cbBaudRateRModem.Text;
            Properties.Settings.Default.RfidTwoReaders = FlgTwoReaders.Checked;
            Properties.Settings.Default.Save();
        }



        private void InitializeComboBoxes()
        {
            var fieldPorts = SerialPort.GetPortNames();
            if (fieldPorts.Length == 0)
            {
                MessageBox.Show(@"В системе не найдены последовательные порты!", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetComboBoxSource(cbPortOut, fieldPorts);
            SetComboBoxSource(cbPortIn, fieldPorts);
            SetComboBoxSource(cbPortRModem, fieldPorts);
            SetComboBoxSource(cbPortOut, fieldPorts);
            var baudRates = new[]
                                {
                                    "1200", 
                                    "2400",
                                    "9600",
                                    "1440",
                                    "19200",
                                    "38400",
                                    "57600",
                                    "115200",
                                };
            SetComboBoxSource(cbBaudRateRModem, baudRates);
        }
        private static void SetComboBoxSource(ComboBox cBox, string[] items)
        {
            if (!items.Equals(cBox.Items))
            {
                cBox.Items.Clear();
                if (items.Length != 0)
                {
                    cBox.Items.AddRange(items);
                    cBox.SelectedIndex = 0;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbPortOut.Text) 
                || string.IsNullOrEmpty(cbPortIn.Text) 
                || string.IsNullOrEmpty(cbPortRModem.Text) 
                || string.IsNullOrEmpty(cbBaudRateRModem.Text))
            {
                MessageBox.Show("Не указан порт или скорость!", "Информация", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            if (cbPortOut.Text.Equals(cbPortIn.Text)
                || cbPortOut.Text.Equals(cbPortRModem.Text)
                || cbPortIn.Text.Equals(cbPortRModem.Text))
            {
                MessageBox.Show("Указан один и тот же порт для разного назначения - это не допускается!", "Информация", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            SaveData();
            DialogResult = DialogResult.OK;
        }

        private void FormSerialSettings_Load(object sender, EventArgs e)
        {
            RetriveData();
            grpOut.Enabled = FlgTwoReaders.Checked;
        }

        private void FlgTwoReaders_CheckedChanged(object sender, EventArgs e)
        {
            var box = sender as CheckBox;
            if (box==null) return;
            grpOut.Enabled = box.Checked;
        }
    }
}
