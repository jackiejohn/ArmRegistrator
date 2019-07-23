using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace ArmRegistrator
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void BtnDbConnect_Click(object sender, EventArgs e)
        {
            var sqlb = new SqlConnectionStringBuilder(Properties.Settings.Default.ConnectionString);
            if (string.IsNullOrEmpty(sqlb.ToString()))
            {
                //Hide();
                AppStaticMethods.CreateAndStoreConnectionString();
                //Close();
                return;
            }
            using (var dialog = AppStaticMethods.CreateDataConnectionDialog())
            {
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.ConnectionString = sqlb.ToString();
                dialog.HelpButton = false;
                //Hide();
                DialogResult dialogResult = DataConnectionDialog.Show(dialog);
                if (dialogResult == DialogResult.OK)
                {
                    sqlb.ConnectionString = dialog.ConnectionString;
                    Properties.Settings.Default["ConnectionString"] = sqlb.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            //Close();
            return;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            var assembly = typeof(Program).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute),true)[0];
            var id = attribute.Value;
            var data=WorkData.DataGenerator.GetData(id);
            var user = Encoding.UTF8.GetString(data.Catalog);
            var sn = Encoding.UTF8.GetString(data.Data);
            using (var dialog = new FormAbout(user,sn))
            {
                //Hide();
                //Close();
                dialog.ShowDialog(this);
            }
            //Close();
            return;
        }
        
    }
}
