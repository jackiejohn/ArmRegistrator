using System;
using System.Drawing;
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
//using Security.Windows.Forms;
//using System.Threading.Tasks;


namespace ArmRegistrator
{
    public static class AppStaticMethods
    {
        public static bool CreateAndStoreConnectionString()
        {
            using (var dialog = CreateDataConnectionDialog())
            {
                // If you want the user to select from any of the available data sources, do this:
                // DataSource.AddStandardDataSources(dialog);

                // OR, if you want only certain data sources to be available
                // (e.g. only SQL Server), do something like this instead: 
                dialog.DataSources.Add(DataSource.SqlDataSource);
                //dialog.DataSources.Add(DataSource.SqlFileDataSource);

                // The way how you show the dialog is somewhat unorthodox; `dialog.ShowDialog()`
                // would throw a `NotSupportedException`. Do it this way instead:
                DialogResult userChoice = DataConnectionDialog.Show(dialog);

                // Return the resulting connection string if a connection was selected:
                if (userChoice == DialogResult.OK)
                {
                    var sqlb = new SqlConnectionStringBuilder(dialog.ConnectionString);
                    // sqlb.Password = string.Empty;
                    Properties.Settings.Default["ConnectionString"] = sqlb.ToString();
                    Properties.Settings.Default.Save();
                    return true;
                }

                return false;
            }
        }

        public static DataConnectionDialog CreateDataConnectionDialog()
        {
            var dialog = new DataConnectionDialog();
            dialog.DataSources.Add(DataSource.SqlDataSource);

            // TODO: русские названия кнопок
            return dialog;
        }

        public static bool IsSavePasswordChecked(this DataConnectionDialog dialog)
        {
            var control = GetPropertyValue("ConnectionUIControl", dialog, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty);
            if (control == null)
            {
                return false;
            }

            var properties = GetPropertyValue("Properties", control, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.DeclaredOnly);
            if (properties == null)
            {
                return false;
            }

            var savePassword = GetPropertyValue("SavePassword", properties, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            if (savePassword != null && savePassword is bool)
            {
                return (bool)savePassword;
            }

            return false;
        }

        private static object GetPropertyValue(string propertyName, object target, BindingFlags bindingFlags)
        {
            var propertyInfo = target.GetType().GetProperty(propertyName, bindingFlags);
            if (propertyInfo == null)
            {
                return null;
            }

            return propertyInfo.GetValue(target, null);
        }

        public static string Connect(Func<SqlConnectionStringBuilder, bool> testConnectionString)
        {
            var sqlb = new SqlConnectionStringBuilder(Properties.Settings.Default.ConnectionString);
            if (testConnectionString(sqlb))
            {
                //using (var dialog = new UserCredentialsDialog("Quarry", "Quarry"))
                //{
                //    dialog.SaveChecked = false;
                //    dialog.User = sqlb.UserID;
                //    dialog.Domain = string.Empty;
                //    dialog.Flags = UserCredentialsDialogFlags.DoNotPersist;

                //    if (dialog.ShowDialog() == DialogResult.OK)
                //    {
                //        sqlb.Password = dialog.PasswordToString();
                //        sqlb.UserID = dialog.User;
                //        var constr = sqlb.ToString();
                //        using (var con = new SqlConnection(constr))
                //        {
                //            // проверим, если не откроется, то будет исключение
                //            con.Open();
                //        }

                //        return sqlb.ToString();
                //    }
                //}
                var constr = sqlb.ToString();
                using (var con = new SqlConnection(constr))
                {
                    // проверим, если не откроется, то будет исключение
                    con.Open();
                }

                return sqlb.ToString();
            }

            return string.Empty;
        }

        public static DialogResult InputBox(string title, string promtText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox=new TextBox();
            Button buttonOk=new Button();
            Button buttonCancel=new Button();

            form.Text = title;
            label.Text = promtText;
            textBox.Text = value;
            
            buttonOk.Text = "Ввод";
            buttonCancel.Text = "Отмена";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9,20,372,13);
            textBox.SetBounds(12,36,372,20);
            buttonOk.SetBounds(228,72,75,23);
            buttonCancel.SetBounds(309,72,75,23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize=new Size(396,107);
            form.Controls.AddRange(new Control[]{label,textBox,buttonOk,buttonCancel});
            form.ClientSize=new Size(Math.Max(300,label.Right+10),form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;

        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}
