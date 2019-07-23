using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ConcurrenceType;
using DataGridViewExtendedControls.DataGridViewProgress;
using DataGridViewExtendedControls.Utils;
using Microsoft.Data.ConnectionUI;
using RadioModule;
using SharedTypes.Paks;
using Timer = System.Windows.Forms.Timer;

namespace ArmRegistrator
{
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
        }

        public void RefreshObjectTable()
        {
            RefreshDataSetTables(_dsQuarry, _adapters, new[]{"Object"});
        }

        #region Form Events
        private void FormReg_Load(object sender, EventArgs e)
        {
            GetDefaultParamValues();
            BtnDbConnectSetImage(false);
            CreateTrackerViewColumns();
            CreateCardViewColumns();
            HaveModemChange(FlagIsNotHaveModem.Checked);
        }
        private void FormReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_rModule != null) _rModule.StopCommunication();
        }
        private void FormReg_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormWidth = Width;
            Properties.Settings.Default.FormHeight = Height;
            Properties.Settings.Default.Save();
        }
        private void FormReg_SizeChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsMaximized != (WindowState == FormWindowState.Maximized))
            {
                Properties.Settings.Default.IsMaximized = WindowState == FormWindowState.Maximized;
                Properties.Settings.Default.Save();
            }
        }

        private void CardView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dgv = (DataGridView)sender;
            var row = ((DataRowView)dgv.Rows[e.RowIndex].DataBoundItem).Row;
            if (row["Name"].ToString() == "InFieldTime" && dgv.Columns[e.ColumnIndex].Name == "Value")
            {
                var dgvTrack = TrackerView;
                if (dgvTrack.CurrentRow != null)
                {
                    var inField = (bool)((DataRowView)dgvTrack.CurrentRow.DataBoundItem).Row["InField"];
                    //var longTime = (bool)((DataRowView)dgvTrack.CurrentRow.DataBoundItem).Row["LongTimeInField"];
                    var style = e.CellStyle;
                    Color col = Color.LightGreen;
                    //if (longTime) col = Color.Orange;
                    if (!inField) col = Color.OrangeRed;
                    style.BackColor = col;
                    e.CellStyle.ApplyStyle(style);
                }
            }
        }
        private void TrackerView_SelectionChanged(object sender, EventArgs e)
        {
            ChangeTrackViewRow();
            //var dgv = (DataGridView)sender;
            //var dgvCard = CardView;
            //if (dgv.CurrentRow == null)
            //{
            //    ButtonsFieldSetEnabled(null);
            //    dgvCard.DataSource = null;
            //    return;
            //}
            //if (dgv.CurrentRow.Index < 0) return;
            ////if (_lastRow == dgv.CurrentRow) return;
            ////_lastRow = dgv.CurrentRow;

            //var row = ((DataRowView)dgv.CurrentRow.DataBoundItem).Row;
            //if (_rModuleConnected) ButtonsFieldSetEnabled((bool)row["InField"]);

            //var objectType = (int)row["ObjectTypeId"];

            //DataTable table = _dsQuarry.Tables["ObjectVT"];

            //if (objectType == 1)
            //{
            //    table = _dsQuarry.Tables["ObjectET"];
            //}
            //if (table == null) return;
            //if (dgvCard.DataSource == null) dgvCard.DataSource = table;
            //else if (!dgvCard.DataSource.Equals(table)) dgvCard.DataSource = table;
            //TransposedTableRefresh(row, table);
        }

        private void BtnReplace_Click(object sender, EventArgs e)
        {
            using (var frm = new FormReplacement(_dsQuarry, this))
            {
                frm.ShowDialog(this);
            }
        }
        private void BtnStartReader_Click(object sender, EventArgs e)
        {
            using (var frm = new FormBar())
            {
                frm.ShowDialog(this);
            }
        }
        private void FlagIsNotHaveModem_Click(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            if (state)
            {
                if (!OperatorRiskAgreeModem())
                {
                    ((CheckBox)sender).Checked = false;
                    return;
                }
            }
            HaveModemChange(state);
            ChangeTrackViewRow();
            SaveHaveModemProp(state);
        }
        private void BtnDbConnect_Click(object sender, EventArgs e)
        {
            if (!_dataConfigured)
            {
                if (!Arm_ConfigDataComponents()) return;
                _dataConfigured = true;
                BtnDbConnectSetImage(true);
                TimerStart();
                BtnReplace.Enabled = true;
            }
            else
            {
                _dataConfigured = false;
                BtnDbConnectSetImage(false);
                TimerStop();
                _dsQuarry.Clear();
                BtnReplace.Enabled = false;
            }
            StaticMethods.DataGridViewSetColumnWidth(TrackerView);
            TrackerViewSetFilter();
        }
        private void BtnDbConfig_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormSettings())
            {
                dialog.ShowDialog(this);
            }
            return;
            
            var sqlb = new SqlConnectionStringBuilder(Properties.Settings.Default.ConnectionString);
            if (string.IsNullOrEmpty(sqlb.ToString()))
            {
                AppStaticMethods.CreateAndStoreConnectionString();
                return;
            }
            using (var dialog = AppStaticMethods.CreateDataConnectionDialog())
            {
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.ConnectionString = sqlb.ToString();
                dialog.HelpButton = false;
                DialogResult dialogResult = DataConnectionDialog.Show(dialog);
                if (dialogResult == DialogResult.OK)
                {
                    sqlb.ConnectionString = dialog.ConnectionString;
                    Properties.Settings.Default["ConnectionString"] = sqlb.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }
        private void BtnAllObjectType_Click(object sender, EventArgs e)
        {
            FlagPersonal.Checked = true;
            FlagTransport.Checked = true;
            FlagTechnics.Checked = true;
            TrackerViewSetFilter();
        }
        private void BtnAllField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = true;
            FlagIsNotInField.Checked = true;
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void BtnRModuleConnect_Click(object sender, EventArgs e)
        {
            if (!_rModuleConnected)
            {
                if (!ConnectToRModule()) return;
                if (_rModule != null) _rModule.StartCommunication();
                _rModuleConnected = true;
                BtnRModuleConnectSetImage(true);
                ChangeTrackViewRow();
            }
            else
            {
                if (_rModule != null) _rModule.StopCommunication();
                _rModuleConnected = false;
                BtnRModuleConnectSetImage(false);
                ButtonsFieldSetEnabled(null);
            }
        }
        private void BtnInField_Click(object sender, EventArgs e)
        {
            ChangeInFieldState();
        }
        private void BtnNotInField_Click(object sender, EventArgs e)
        {
            ChangeInFieldState();
        }

        private void FlagPersonal_Click(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagTransport_CheckedChanged(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagTechnics_CheckedChanged(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagIsInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void FlagIsNotInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void FlagLongTimeInField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = false;
            FlagIsNotInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }

        private void ToolStripTextBoxHours_Validated(object sender, EventArgs e)
        {
            Properties.Settings.Default.LongTime = ((ToolStripTextBox)sender).Text;
            Properties.Settings.Default.Save();
            TrackerViewSetFilter();
        }
        private void ToolStripTextBoxHours_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string strVal = ((ToolStripTextBox)sender).Text;
            int val;
            if (int.TryParse(strVal, out val))
            {
                if (val >= 1 && val <= 99) return;
            }
            e.Cancel = true;
            MessageBox.Show("Вводимое значение должно быть целым числом от 1 до 99", "Неверные данные",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void ToolStripTextBoxHours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Validate();
                e.Handled = true;
                return;
            }
        }
        private void ToolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (_dsQuarry == null) return;
            Image img = Properties.Resources.SearchIco;
            if (ToolStripTextBoxSearch.Text.Length > 0) img = Properties.Resources.DelCross;
            ToolStripButtonSearch.Image = img;

            SetFullFilter(ToolStripTextBoxSearch.Text);
        }
        private void ToolStripButtonSearch_Click(object sender, EventArgs e)
        {
            ToolStripTextBoxSearch.Clear();
        }

        private void RModule_OnErrReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RModule_OnAckReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RModule_OnDataReceived(object sender, EventArgs e)
        {
            _mreHaveData.Set();
        }
        private void RModule_OnPortError(object sender, EventArgs e)
        {
            _rModule.StopCommunication();
            _rModule.Dispose();
            _rModule = null;
            _rModuleConnected = false;
            BtnRModuleConnectSetImage(false);
            ButtonsFieldSetEnabled(null);
        }

        #endregion

        private void GetDefaultParamValues()
        {
            ToolStripTextBoxHours.Text = Properties.Settings.Default.LongTime;
            WindowState = Properties.Settings.Default.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Width = Properties.Settings.Default.FormWidth;
            Height = Properties.Settings.Default.FormHeight;
            FlagIsNotHaveModem.Checked = Properties.Settings.Default.HaveModemState;
        }
        private void CreateCardViewColumns()
        {
            var dgv = CardView;
            var columns = FormRegHelper.GetDefaultCardColumnTitles();
            StaticMethods.CreateDataGridViewColumn(dgv, columns);
            var colValue = dgv.Columns["Value"];
            if (colValue ==null ) throw new NullReferenceException("Столбец Value не найден");
            colValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void CreateTrackerViewColumns()
        {
            var dgv = TrackerView;
            var columns = FormRegHelper.GetVisibleTrackerColumnNames();
            StaticMethods.CreateDataGridViewColumn(dgv, columns);
            TrackerViewAddProgressColumns();
            TrackerViewAddCheckBoxColumns();
        }
        
        private void TrackerViewAddCheckBoxColumns()
        {
            var colsName = new[] { "InField", "Error" };
            StaticMethods.DataGridViewChangeColumnsToCheckBox(TrackerView, colsName);
        }
        private void TrackerViewAddProgressColumns()
        {
            DataGridView dgv = TrackerView;
            if (dgv.Columns == null) return;

            const string colName = "Charge";
            var col = dgv.Columns[colName];
            if (col==null) return;
            int indxCharge = col.Index;

            DataGridViewProgressCell cell = GetDefaultProgressCell();
            cell.BarStyle = ProgressCellProgressStyle.Visible;
            var progressColumn = new DataGridViewProgressColumn(cell)
            {
                Name = colName,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DataPropertyName = colName,
                CellTemplate = cell,
                HeaderText = col.HeaderText,
            };
            dgv.Columns.Remove(colName);
            dgv.Columns.Insert(indxCharge - 1, progressColumn);

        }
        
        private void TrackerViewSetFilter()
        {
            DataGridView dgv = TrackerView;
            var bs = (BindingSource)dgv.DataSource;
            if (bs == null) return;
            var sbType = new StringBuilder(" or 1<>1");
            if (FlagPersonal.Checked) sbType.Append(" or ObjectTypeId=1");
            if (FlagTransport.Checked) sbType.Append(" or ObjectTypeId=2");
            if (FlagTechnics.Checked) sbType.Append(" or ObjectTypeId=3");
            if (sbType.Length > 0)
            {
                sbType.Replace(" or ", "", 0, 5);
                sbType.Insert(0, "(");
                sbType.Append(")");
            }

            var sbField = new StringBuilder(" or 1<>1");
            if (FlagIsInField.Checked) sbField.Append(" or InField=1");
            if (FlagIsNotInField.Checked) sbField.Append(" or InField=0");
            if (FlagLongTimeInField.Checked) sbField.AppendFormat(" or LongTime>={0}", ToolStripTextBoxHours.Text);
            if (sbField.Length > 0)
            {
                sbField.Replace(" or ", "", 0, 5);
                // sbField.Insert(0, "(");
                //sbField.Append(")");
            }
            sbType.AppendFormat("and({0}) and (ServiceId>0)", sbField);
            _filter = sbType.ToString();
            SetFullFilter(ToolStripTextBoxSearch.Text);
            //bs.Filter = sbType.ToString();
        }
        

        private void ChangeTrackViewRow ()
        {
            var dgv = TrackerView;
            var dgvCard = CardView;
            if (dgv.CurrentRow == null)
            {
                ButtonsFieldSetEnabled(null);
                dgvCard.DataSource = null;
                return;
            }
            if (dgv.CurrentRow.Index < 0) return;

            var row = ((DataRowView)dgv.CurrentRow.DataBoundItem).Row;
            if (_rModuleConnected) ButtonsFieldSetEnabled((bool)row["InField"]);

            var objectType = (int)row["ObjectTypeId"];

            DataTable table = _dsQuarry.Tables["ObjectVT"];

            if (objectType == 1)
            {
                table = _dsQuarry.Tables["ObjectET"];
            }
            if (table == null) return;
            if (dgvCard.DataSource == null) dgvCard.DataSource = table;
            else if (!dgvCard.DataSource.Equals(table)) dgvCard.DataSource = table;
            TransposedTableRefresh(row, table);
        }
        
        

        private void TimerStart()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Tick += Timer_Tick;
            }
            int interval = Properties.Settings.Default.RefreshTime * 1000;
            if (interval == 0) return;
            _timer.Interval = interval;
            _timer.Start();
        }
        private void TimerStop()
        {
            if (_timer !=null) _timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RefreshDataSetTables(_dsQuarry, _adapters,null);
        }

        private bool TimerIsStarted()
        {
            if (_timer==null) return false;
            return _timer.Enabled;
        }

        private static DataGridViewProgressCell GetDefaultProgressCell()
        {
            return new DataGridViewProgressCell
                       {
                HiLevelColor = Color.FromArgb(203, 235, 108),
                LowLevelColor = Color.Red,
                MaxLimit = 15,
                MidPoint = 2,
                TextStyle = ProgressCellTextStyle.Percentage,
                BarStyle = ProgressCellProgressStyle.Invisible
            };
        }
        
        private void RefreshDataSetTables(DataSet dataSet, Dictionary<string, SqlDataAdapter> adapters, IEnumerable<string> tblNames)
        {
            //var tblNames = new[] { "Object" };
            if (tblNames == null) tblNames = adapters.Keys;
            try
            {
                foreach (string tblName in tblNames)
                {
                    adapters[tblName].Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                var timerState = TimerIsStarted();
                TimerStop();
                MessageBox.Show(string.Format("Возникла ошибка при обновлении данных." + Environment.NewLine
                                              + "Текст ошибки: {0}", ex.Message), "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (timerState) TimerStart();
            }
        }
        
        private bool Arm_ConfigDataComponents()
        {
            var conn = new SqlConnection
            {
                ConnectionString = Properties.Settings.Default.ConnectionString
            };
            _adapters.Clear();
            try
            {
                conn.Open();
                ConfigDataCreateDataAdapter(_dsQuarry, conn, "Object", "pa_ArmRegistrSelect", new KeyValuePair<string, SqlDbType>("ObjectId", SqlDbType.Int), _adapters);
                SetPrimaryKeyOnTable(_dsQuarry, "Object", "ObjectId");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка работы с базой данных." + Environment.NewLine
                    + "Текст ошибки:" + ex.Message, "Подключение к БД", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var dict = new Dictionary<string, string>
                           {
                               {"Object","ObjectId"},
                           };
            ConfigDataAutoIncrementColumns(_dsQuarry, dict);

            const string tableName = "Object";
            var bsP = new BindingSource(_dsQuarry, tableName);
            TrackerView.DataSource = bsP;
            
            var vehicleColumns = new []
                                      {
                                          _dsQuarry.Tables[tableName].Columns["_Number"],
                                          _dsQuarry.Tables[tableName].Columns["VehicleTypeName"],
                                          _dsQuarry.Tables[tableName].Columns["FuelLevelMax"],
                                          _dsQuarry.Tables[tableName].Columns["ObjectId"],
                                          _dsQuarry.Tables[tableName].Columns["Code"],
                                          _dsQuarry.Tables[tableName].Columns["ServiceName"],
                                          _dsQuarry.Tables[tableName].Columns["Chief"],
                                          _dsQuarry.Tables[tableName].Columns["Phone"],
                                          _dsQuarry.Tables[tableName].Columns["InFieldTime"],
                                          
                                      };
            Dictionary<string, string> columnTitles = FormRegHelper.GetDefaultTrackerColumnTitles();
            CreateTransposedTable(_dsQuarry, vehicleColumns, string.Format("{0}VT", tableName), columnTitles);
            var employeeColumns = new[]
                                      {
                                          _dsQuarry.Tables[tableName].Columns["Surname"],
                                          _dsQuarry.Tables[tableName].Columns["Name"],
                                          _dsQuarry.Tables[tableName].Columns["Patronymic"],
                                          _dsQuarry.Tables[tableName].Columns["Position"],
                                          _dsQuarry.Tables[tableName].Columns["_Number"],
                                          _dsQuarry.Tables[tableName].Columns["ObjectId"],
                                          _dsQuarry.Tables[tableName].Columns["Code"],
                                          _dsQuarry.Tables[tableName].Columns["ServiceName"],
                                          _dsQuarry.Tables[tableName].Columns["Chief"],
                                          _dsQuarry.Tables[tableName].Columns["Phone"],
                                          _dsQuarry.Tables[tableName].Columns["InFieldTime"],
                                      };
            CreateTransposedTable(_dsQuarry, employeeColumns, string.Format("{0}ET", tableName), columnTitles);
            return true;
        }

        private static void CreateTransposedTable(DataSet ds, IEnumerable<DataColumn> columns, string transTableName, Dictionary<string, string> columnsTitle)
        {
            if (columns == null) return;
            if (ds == null) return;
            var table = ds.Tables[transTableName];
            if (table == null)
            {
                table = new DataTable(transTableName);
                var keyColumn = table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("Value", typeof(object));
                table.Columns.Add("Button", typeof(bool));
                table.PrimaryKey = new[] { keyColumn };
                ds.Tables.Add(table);
            }

            var buttonCollection = FormRegHelper.GetDefaultColumnsWithButton(); //TODO: возможно не надо так как коллекция пустая
            foreach (DataColumn column in columns)
            {
                if (columnsTitle.ContainsKey(column.ColumnName))
                {
                    DataRow row = table.NewRow();
                    row["Title"] = columnsTitle[column.ColumnName];
                    row["Name"] = column.ColumnName;
                    row["Button"] = buttonCollection.Contains(column.ColumnName) ? true : false;
                    table.Rows.Add(row);
                }
            }
            return;
        }

        
        private static void ConfigDataCreateDataAdapter(DataSet dataSet, SqlConnection conn, string tableName, string selCommand, KeyValuePair<string, SqlDbType> keyCol, Dictionary<string, SqlDataAdapter> adapters)
        {
            SqlDataAdapter adp = CreateDataAdapter(dataSet, conn, tableName, selCommand, keyCol);
            if (adp != null) adapters.Add(tableName, adp);
        }
        private static SqlDataAdapter CreateDataAdapter(DataSet dataSet, SqlConnection conn, string tableName, string selCommand, KeyValuePair<string, SqlDbType> keyCol)
        {
            var adapter = new SqlDataAdapter { SelectCommand = conn.CreateCommand() };

            adapter.SelectCommand.CommandText = selCommand;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.TableMappings.Add("Table", tableName);
            //if (tableName.Equals("Object"))
            //{
            //    adapter.FillSchema(dataSet, SchemaType.Mapped);
            //    dataSet.Tables["Object"].Columns["Time"].DateTimeMode = DataSetDateTime.Utc;
            //}
            adapter.Fill(dataSet);

            Dictionary<string, SqlDbType> cols = DbWrapper.GetColumnsDictionary(dataSet, tableName);

            var sb = new StringBuilder();
            var sbPar = new StringBuilder();
            sb.AppendFormat("INSERT INTO {0} (", tableName);
            foreach (var col in cols)
            {
                sb.AppendFormat("{0},", col.Key);
                sbPar.AppendFormat("@{0},", col.Key);
            }
            sb.Remove(sb.Length - 1, 1);
            sbPar.Remove(sbPar.Length - 1, 1);
            sb.AppendFormat(") VALUES({0})", sbPar);

            adapter.InsertCommand = conn.CreateCommand();
            adapter.InsertCommand.CommandText = sb.ToString();
            AddParams(adapter.InsertCommand, cols);

            sb.Remove(0, sb.Length);
            sb.AppendFormat("UPDATE {0} SET ", tableName);
            if (cols.ContainsKey(keyCol.Key)) cols.Remove(keyCol.Key);
            foreach (KeyValuePair<string, SqlDbType> col in cols)
            {
                sb.AppendFormat("{0}=@{1},", col.Key, col.Key);
            }
            sb.Remove(sb.Length - 1, 1);
            cols.Add(keyCol.Key, keyCol.Value);
            sb.AppendFormat(" WHERE {0}=@{1}", keyCol.Key, keyCol.Key);
            adapter.UpdateCommand = conn.CreateCommand();
            adapter.UpdateCommand.CommandText = sb.ToString();
            AddParams(adapter.UpdateCommand, cols);

            sb.Remove(0, sb.Length);
            sb.AppendFormat("DELETE FROM {0} WHERE {1}=@{2}", tableName, keyCol.Key, keyCol.Key);
            adapter.DeleteCommand = conn.CreateCommand();
            adapter.DeleteCommand.CommandText = sb.ToString();
            var keyPar = new Dictionary<string, SqlDbType> { { keyCol.Key, keyCol.Value } };
            AddParams(adapter.DeleteCommand, keyPar);
            return adapter;
        }
        private static void SetPrimaryKeyOnTable(DataSet dataSet, string tableName, string keyCol)
        {
            dataSet.Tables[tableName].PrimaryKey = new[] { dataSet.Tables[tableName].Columns[keyCol] };
        }
        private static void AddParams(SqlCommand cmd, Dictionary<string, SqlDbType> cols)
        {
            foreach (KeyValuePair<string, SqlDbType> col in cols)
            {
                cmd.Parameters.Add("@" + col.Key, col.Value, 0, col.Key);
            }
        }
        private static void ConfigDataAutoIncrementColumns(DataSet dataSet, Dictionary<string, string> tableKeyDict)
        {

            foreach (KeyValuePair<string, string> pair in tableKeyDict)
            {
                DataRow[] drs = dataSet.Tables[pair.Key].Select("", string.Format("{0} desc", pair.Value));
                int tmp=0;
                if (drs.Length>0)
                {
                    DataRow dr = drs[0];
                    int.TryParse(dr[pair.Value].ToString(), out tmp);
                }
                
                DataColumn dt = dataSet.Tables[pair.Key].Columns[pair.Value];
                dt.AutoIncrement = true;
                dt.AutoIncrementSeed = tmp + 1;
            }
        }
        private static void TransposedTableRefresh(DataRow row, DataTable transTable)
        {
            if (transTable == null) return;
            foreach (DataRow dataRow in transTable.Rows)
            {
                var name = (string)dataRow["Name"];
                dataRow["Value"] = row[name];
            }
            var rows = transTable.Select("Name='InFieldTime'");
            if (rows.Length==0)return;
            DataRow transRow = rows[0];
            DateTime newTime;
            var ci = new CultureInfo("en-US");
            var parsed = DateTime.TryParse(transRow["Value"].ToString(), ci, DateTimeStyles.AssumeUniversal,out newTime);
            if (parsed) transRow["Value"] = newTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            return;
        }
        
        
        private void BtnDbConnectSetImage(bool isConnected)
        {
            var newImg = isConnected ? Properties.Resources.ImageConnectionActive : Properties.Resources.ImageConnectionDeactive;
            if (BtnRModuleConnect.InvokeRequired)
            {
                BtnDbConnect.BeginInvoke(new Action<Bitmap>(img => { BtnRModuleConnect.Image = img; }), newImg);
            }
            else
            {
                BtnDbConnect.Image = newImg;
            }
        }
        private void BtnRModuleConnectSetImage(bool isConnected)
        {
            var newImg = isConnected ? Properties.Resources.RModuleConnected : Properties.Resources.RModuleNotConnected;
            if(BtnRModuleConnect.InvokeRequired)
            {
                BtnRModuleConnect.BeginInvoke(new Action<Bitmap>(img =>{ BtnRModuleConnect.Image = img; }), newImg);
            }
            else
            {
                BtnRModuleConnect.Image = newImg;
            }
            
        }
        private void FlagIsNotHaveModemSetImage(bool haveModem)
        {
            var newImg = haveModem ? Properties.Resources.ModemHave : Properties.Resources.ModemNotHave;
            if (FlagIsNotHaveModem.InvokeRequired)
            {
                FlagIsNotHaveModem.BeginInvoke(new Action<Bitmap>(img => { FlagIsNotHaveModem.Image = img; }), newImg);
            }
            else
            {
                FlagIsNotHaveModem.Image = newImg;
            }

        }
        
        private void ChangeInFieldState()
        {
            if (TrackerView.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Выделено несколько строк! Изменения коснутся только одиного"
                    + Environment.NewLine + "Продолжаем?", "Обратите внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            var row = StaticMethods.GetCurrentDataRow(TrackerView);
            if (row == null) return;

            int objectId = Convert.ToInt32(row["ObjectId"]);
            int trakObjId = objectId;

            if (!Convert.IsDBNull(row["_ActiveObjectId"])) trakObjId = Convert.ToInt32(row["_ActiveObjectId"]);
            bool inField = Convert.ToBoolean(row["InField"]);
            
            bool lampMode = true;
            bool workMode = true;
            bool sended=true;
            bool addEvent = true;
            if (!_isNoModem)
            {
                UInt16 status;
                Cursor = Cursors.WaitCursor;
                sended = GetStatusFromObject(out status, trakObjId);
                Cursor = Cursors.Default;
                var statusWord = new PakStatusWord(status);
                lampMode = statusWord.LampMode;
                workMode = sended && !(statusWord.Error || statusWord.Charge < 9);
                
                if(!inField)
                {
                    var sb = new StringBuilder("Изменение режима НЕ ДОПУСТИМО по следующим причинам:");
                    sb.AppendLine();
                    if (!sended) sb.AppendLine(" - нет связи с трекером");
                    else
                    {
                        if (statusWord.Error) sb.AppendLine(" - ошибка оборудования");
                        if (statusWord.Charge < 9) sb.AppendLine(" - низкий заряд батареи");
                    }
                    MessageBox.Show(sb.ToString(), "Изменение режима", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                addEvent = false;
                if(!sended)
                {
                    if (!OperatorRiskAgree()) return;
                    addEvent = true;
                }
            }

            Cursor = Cursors.WaitCursor;
            bool retVal;
            if (inField) retVal = InFieldUnSet(!(_isNoModem || lampMode || !sended), addEvent, objectId, trakObjId);
            else retVal = InFieldSet(!(_isNoModem || workMode||!sended), addEvent, objectId, trakObjId);
            Cursor = Cursors.Default;

            if (retVal)
            {
                RefreshDataSetTables(_dsQuarry, _adapters, null);
                ChangeTrackViewRow();
            }
        }
        private bool InFieldUnSet(bool setWorkMode, bool addEvent, int objectId, int trakObjId )
        {
            bool retVal=true;
            UInt16 status;
            if (setWorkMode) retVal = SendObjectPassiveMode(out status, trakObjId);
            if (addEvent) retVal = ExecSql_AddEvent(objectId, "Со смены");
            
            if (retVal) retVal = ExecSql_SetObjectInField(objectId, true);
            return retVal;
        }

        private bool InFieldSet(bool setLampMode, bool addEvent, int objectId, int trakObjId)
        {
            bool retVal = true;
            if (setLampMode)
            {
                UInt16 status;
                retVal = SendObjectWorkMode(out status, trakObjId);
                retVal = retVal && !PakStatusWord.Instance(status).LampMode;
            }
    
            if (addEvent) retVal = ExecSql_AddEvent(objectId, "На смену");
            if (retVal) retVal = ExecSql_SetObjectInField(objectId, false);
            return retVal;
        }

        private bool ConnectToRModule()
        {
            var config = Configuration.GetDefault();
            string portName = Properties.Settings.Default.ComPortName;
            string baudRate = Properties.Settings.Default.ComPortBaudRate;

            bool tryConnect = true;
            while (tryConnect)
            {
                if (string.IsNullOrEmpty(portName))
                {
                    string[] portSettings = ChoiceComPort();
                    portName = portSettings[0];
                    baudRate = portSettings[1];
                    if (string.IsNullOrEmpty(portName)) return false;
                }
                
                if (_rModule==null)
                {
                    _rModule = new RModule(portName, config) { BaudRate = Convert.ToInt32(baudRate) };//{BaudRate = 9600};//TODO: Зделать возможность выбора скорости
                    //var parser = new Parser(new[] { "OK\r", "ERROR\r" }, ModulePak.PakSize, ModulePak.AddressBytesCount);
                    //_rModule.SetParser(parser);
                }
                if (_rModule.IsInit) return true;
                
                tryConnect = false;
                var initResult = _rModule.InitRModule();
                if (!string.IsNullOrEmpty(initResult))
                {
                    MessageBox.Show("Не удается инициализировать радиомодем на порту " + portName,
                                    "Ошибка инициализации радиомодуля (" + initResult + ")", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    portName = string.Empty;
                    tryConnect = true;
                }
                _rModule.SetDefaultParser();
            }
            Properties.Settings.Default.ComPortName = portName;
            Properties.Settings.Default.ComPortBaudRate = baudRate;
            Properties.Settings.Default.Save();
            _rModule.OnPortError += RModule_OnPortError;
            _rModule.OnDataReceived += RModule_OnDataReceived;
            //_rModule.OnAckReceived += RModule_OnAckReceived;
            //_rModule.OnErrReceived += RModule_OnErrReceived;
            return true;
        }

        private string[] ChoiceComPort()
        {
            var frm = new ChoisePortDlg(Properties.Settings.Default.ComPortName, Properties.Settings.Default.ComPortBaudRate);
            DialogResult result = frm.ShowDialog(this);
            var resString= new string[2];
            if (result == DialogResult.OK)
            {
                resString[0] = frm.FieldPort.Text;
                resString[1] = frm.FieldBaudRate.Text;
            }
            frm.Dispose();
            return resString;
        }

        private void InputHoursVisibility()
        {
            bool isVisible = FlagLongTimeInField.Checked;
            ToolStripTextBoxHours.Visible = isVisible;
            ToolStripLabelHoursPre.Visible = isVisible;
            ToolStripLabelHoursPost.Visible = isVisible;
        }
        private void ButtonsFieldSetEnabled(object commandEnable)
        {
            if (commandEnable == null)
            {
                BtnInField.Enabled = false;
                BtnNotInField.Enabled = false;
                return;
            }
            var enableBtn = (bool)commandEnable;
            BtnInField.Enabled = !enableBtn;
            BtnNotInField.Enabled = enableBtn;
        }

        private void SetFullFilter(string searchFilter)
        {
            var bs = (BindingSource)TrackerView.DataSource;
            if (bs==null) return;
            searchFilter=searchFilter.Replace("'", "");
            bs.Filter = String.Format("({0} like '%{1}%' or {2} like '%{1}%') and ({3}) ", "_Object", searchFilter, "Code", _filter);
        }

        private static bool OperatorRiskAgree()
        {
            return MessageBox.Show("Трекер не доступен. Изменяем режим под вашу ответственность?"
                                   , "Изменение режима объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                   DialogResult.Yes;
        }

        private static bool OperatorRiskAgreeModem()
        {
            return MessageBox.Show("АРМ прекращает использование радиомодема." 
                + Environment.NewLine+ "Изменения режимов будут проводиться под вашей ответственностью." 
                + Environment.NewLine+ "Продолжаем?"
                                   , "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                   DialogResult.Yes;
        }

        private bool SendCommandToObject(out UInt16 status, UInt16 adr, PakCommands command, TimeSpan waitTime )
        {
            status = default(UInt16);
            var pakReqCoord = new PakStructRequest();
            

            // Пакет запроса координат
            const ushort pakNumber = UInt16.MaxValue;
            pakReqCoord.SetRequestCoordinate(adr, pakNumber, command, 0, 0);
            pakReqCoord.ActualDateTime();
            pakReqCoord.WriteCrc16();
            var tpReq = new ModulePak(pakReqCoord,false);
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
            Debug.WriteLine("RetryCount " +i);
            return false;
        }
        private bool GetStatusFromObject(out UInt16 status, int objectId)
        {
            //return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.RequestCoord);
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.RequestStatus, TimeSpan.FromMilliseconds(500));
        }

        private bool SendObjectPassiveMode(out UInt16 status, int objectId)
        {
            return SendCommandToObject(out status, (UInt16) (objectId), PakCommands.SetModePassive,TimeSpan.FromSeconds(1));
        }

        private bool SendObjectWorkMode(out UInt16 status, int objectId)
        {
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.SetModeWork, TimeSpan.FromSeconds(3));
        }

        private bool ExecSql_SetObjectInField(int objectId, bool currentFieldState)
        {
            string conStr = Properties.Settings.Default.ConnectionString;
            bool retVal = true;
            using (var connection = new SqlConnection(conStr))
            {
                try
                {
                    connection.Open();
                    var cmd=connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText ="p_SetObjectInField";
                    cmd.Parameters.AddWithValue("@ObjectId", objectId);
                    cmd.Parameters.AddWithValue("@InField", !currentFieldState);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    var timerState = TimerIsStarted();
                    TimerStop();
                    MessageBox.Show(
                        "Ошибка выполнения процедуры." + Environment.NewLine + "Текст сообщения:" + ex.Message
                        , "Ошибка выполнения SQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (timerState) TimerStart();
                    
                    retVal = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
        }
        private bool ExecSql_AddEvent(int objectId, string value)
        {
            string conStr = Properties.Settings.Default.ConnectionString;
            bool retVal = true;
            using (var connection = new SqlConnection(conStr))
            {
                try
                {
                    connection.Open();
                    var cmd=connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText ="p_AddEvent";
                    cmd.Parameters.AddWithValue("@EventTypeId", 1);
                    cmd.Parameters.AddWithValue("@Message", "Изменение режима объекта без непосредственного контроля");
                    cmd.Parameters.AddWithValue("@Source", "АРМ-Р");
                    cmd.Parameters.AddWithValue("@ObjectId", objectId);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    var timerState = TimerIsStarted();
                    TimerStop();
                    MessageBox.Show(
                        "Ошибка выполнения процедуры." + Environment.NewLine + "Текст сообщения:" + ex.Message
                        , "Ошибка выполнения SQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (timerState) TimerStart();
                    retVal = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
        }

        

        private static void SaveHaveModemProp(bool state)
        {
            Properties.Settings.Default.HaveModemState = state;
            Properties.Settings.Default.Save();
        }

        private void HaveModemChange(bool state)
        {
            _rModuleConnected = true;
            BtnRModuleConnect.Enabled = !state;
            _isNoModem = state;
            FlagIsNotHaveModemSetImage(!state);
            if (state) { BtnRModuleConnectSetImage(true); }
            else { BtnRModuleConnect_Click(null, null); }
        }

        

        #region Private Fields
        private bool _dataConfigured;
        private bool _rModuleConnected;
        private readonly DataSet _dsQuarry = new DataSet("QuarryDB");
        private readonly Dictionary<string, SqlDataAdapter> _adapters = new Dictionary<string, SqlDataAdapter>();
        private Timer _timer;
        private string _filter = string.Empty;
        private RModule _rModule;
        private readonly ManualResetEvent _mreHaveData = new ManualResetEvent(true);
        private bool _isNoModem;
        #endregion
    }
}
