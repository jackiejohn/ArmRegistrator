using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DataGridViewExtendedControls.DataGridViewProgress;
using Microsoft.Data.ConnectionUI;
using RadioModule;
using SharedTypes.Paks;
using SharedTypes.Queue;
using Timer = System.Windows.Forms.Timer;

namespace ArmRegistrator
{
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
        }

        private void FormReg_Load(object sender, EventArgs e)
        {
            GetDefaultParamValues();
            BtnDbConnectSetImage(false);
            CreateTrackerViewColumns();
            CreateCardViewColumns();
        }
        private void FormReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_rModule != null) _rModule.StopCommunication();
        }
        private void GetDefaultParamValues()
        {
            ToolStripTextBoxHours.Text = Properties.Settings.Default.LongTime;
            WindowState = Properties.Settings.Default.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Width = Properties.Settings.Default.FormWidth;
            Height = Properties.Settings.Default.FormHeight;
        }
        private void CreateCardViewColumns()
        {
            var dgv = CardView;
            var columns = GetDefaultCardColumnTitles();
            CreateDataGridViewColumn(dgv, columns);
            dgv.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void CreateTrackerViewColumns()
        {
            var dgv = TrackerView;
            var columns = GetVisibleTrackerColumnTitles();
            CreateDataGridViewColumn(dgv, columns);
            TrackerViewAddProgressColumns();
            TrackerViewAddCheckBoxColumns();
        }
        
        private void TrackerViewAddCheckBoxColumns()
        {
            DataGridView dgv = TrackerView;
            if (dgv.Columns == null) return;

            
            var colsName = new[] { "InField", "Error" };
            foreach (string colName in colsName)
            {
                int indxCharge = dgv.Columns[colName].Index;

                var checkColumn = new DataGridViewCheckBoxColumn()
                {
                    Name = colName,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    DataPropertyName = colName,
                    HeaderText = dgv.Columns[colName].HeaderText,
                };
                dgv.Columns.Remove(colName);
                dgv.Columns.Insert(indxCharge, checkColumn);
            }
            
        }
        private void TrackerViewAddProgressColumns()
        {
            DataGridView dgv = TrackerView;
            if (dgv.Columns == null) return;

            const string colName = "Charge";
            int indxCharge = dgv.Columns[colName].Index;

            DataGridViewProgressCell cell = GetDefaultProgressCell();
            cell.BarStyle = ProgressCellProgressStyle.Visible;
            var progressColumn = new DataGridViewProgressColumn(cell)
            {
                Name = colName,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DataPropertyName = colName,
                CellTemplate = cell,
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                HeaderText = dgv.Columns[colName].HeaderText,
            };
            dgv.Columns.Remove(colName);
            dgv.Columns.Insert(indxCharge - 1, progressColumn);

        }
        private void TrackerViewSetColumnWidth()
        {
            DataGridView dgv = TrackerView;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.Width = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
            }
        }
        private void TrackerViewSetFilter()
        {
            DataGridView dgv = TrackerView;
            BindingSource bs = (BindingSource)dgv.DataSource;
            if (bs == null) return;
            StringBuilder sbType = new StringBuilder(" or 1<>1");
            if (FlagPersonal.Checked) sbType.Append(" or ObjectTypeId=1");
            if (FlagTransport.Checked) sbType.Append(" or ObjectTypeId=2");
            if (FlagTechnics.Checked) sbType.Append(" or ObjectTypeId=3");
            if (sbType.Length > 0)
            {
                sbType.Replace(" or ", "", 0, 5);
                sbType.Insert(0, "(");
                sbType.Append(")");
            }

            StringBuilder sbField = new StringBuilder(" or 1<>1");
            if (FlagIsInField.Checked) sbField.Append(" or InField=1");
            if (FlagIsNotInField.Checked) sbField.Append(" or InField=0");
            if (FlagLongTimeInField.Checked) sbField.AppendFormat(" or LongTime>={0}", ToolStripTextBoxHours.Text);
            if (sbField.Length > 0)
            {
                sbField.Replace(" or ", "", 0, 5);
                // sbField.Insert(0, "(");
                //sbField.Append(")");
            }
            sbType.AppendFormat("and({0})", sbField);
            _filter = sbType.ToString();
            SetFullFilter(ToolStripTextBoxSearch.Text);
            //bs.Filter = sbType.ToString();
        }
        private void TrackerView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dgvCard = CardView;
            if (dgv.CurrentRow == null)
            {
                ButtonsFieldSetEnabled(null);
                dgvCard.DataSource = null;
                return;
            }
            if (dgv.CurrentRow.Index < 0) return;
            //if (_lastRow == dgv.CurrentRow) return;
            //_lastRow = dgv.CurrentRow;

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
            TransposedTableRefresh(((DataRowView)dgv.CurrentRow.DataBoundItem).Row, table);
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

        private void FArm_StartTimer()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Tick += FArm_TimerTick;
            }
            int interval = Properties.Settings.Default.RefreshTime * 1000;
            if (interval == 0) return;
            _timer.Interval = interval;
            _timer.Start();
        }
        private void FArm_StopTimer()
        {
            _timer.Stop();
        }
        private void FArm_TimerTick(object sender, EventArgs e)
        {
            RefreshDataSetTable(_dsQuarry, _adapters, TrackerView);
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
        
        private static Dictionary<string, string> GetDefaultTrackerColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"InField", "На смене"},
                                                     {"ObjectId", "ID"},
                                                     {"_Number", "Номер"},
                                                     {"Code", "Маркер"},
                                                     {"_Object", "Объект"},
                                                     {"ObjectTypeName", "Тип"},
                                                     {"ServiceName", "Служба"},
                                                     {"Description", "Примечания"},
                                                     {"Charge", "Батарея"}, 
                                                     {"FuelLevel", "Уровень топлива"},
                                                     {"Error", "Устр-во неисправно"},
                                                     {"ErrorCode", "Код ошибки"},
                                                     {"VehicleTypeName", "Тип ТС"},
                                                     {"FuelLevelMax", "Уровень топлива(max)"},
                                                     {"Chief", "Ответственный"},
                                                     {"Phone", "Телефон"},
                                                     {"Surname", "Фамилия"},
                                                     {"Name", "Имя"},
                                                     {"Patronymic", "Отчество"},
                                                     {"Position", "Должность"},
                                                     {"LongTimeInField","Более 12 ч."},
                                                     {"InFieldTime","Время смены"}
                                                     
                                                 };
        }
        private static Dictionary<string, string> GetVisibleTrackerColumnTitles()
        {
            var dic = GetDefaultTrackerColumnTitles();
            var columnNames = new[]
                                  {
                                      "InField", "_Number", "Code", "_Object", "ObjectTypeName", "Charge"
                                      , "Error", "ErrorCode",
                                  };
            var newDic = new Dictionary<string, string>();
            foreach (string columnName in columnNames)
            {
                string desc = string.Empty;
                if (dic.ContainsKey(columnName)) desc = dic[columnName];
                newDic.Add(columnName,desc);
            }
            return newDic;
        }
        private static Dictionary<string,string> GetDefaultCardColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"Title", "Параметр"},
                                                     {"Value", "Значение"},
                                                 };
        }
        
        private static void CreateDataGridViewColumn(DataGridView dgv, Dictionary<string, string> columns)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn lastColumn = null;
            foreach (KeyValuePair<string, string> pair in columns)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = pair.Key,
                    DataPropertyName = pair.Key,
                    HeaderText = pair.Value,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    //AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                };
                lastColumn = column;
                dgv.Columns.Add(column);
            }
            //if (lastColumn!=null) lastColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        
        private static void RefreshDataSetTable(DataSet dataSet, Dictionary<string, SqlDataAdapter> adapters, DataGridView dgv)
        {
            var tblNames = new[] { "Object" };
            try
            {
                foreach (string tblName in tblNames)
                {
                    adapters[tblName].Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Возникла ошибка при обновлении данных." + Environment.NewLine
                                              + "Текст ошибки: {0}", ex.Message), "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //if (dgv.CurrentRow != null) TransposedTableRefresh(((DataRowView)dgv.CurrentRow.DataBoundItem).Row, dataSet.Tables["ObjectT"]);
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
            Dictionary<string, string> columnTitles = GetDefaultTrackerColumnTitles();
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

            
            
            //CardView.DataSource = CreateTransposedTable(_dsQuarry, _dsQuarry.Tables[tableName], string.Format("{0}T", tableName), columnTitles);
            //CardViewConfigColumns();
            //RModulesViewConfigColumnAndTitles(columnTitles);

            return true;
        }
        private static DataTable CreateTransposedTable(DataSet ds, IEnumerable<DataColumn> columns,string transTableName, Dictionary<string, string> columnsTitle)
        {
            if (columns == null) return null;
            if (ds == null) return null;
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
            
            var buttonCollection = GetDefaultColumnsWithButton(); //TODO: возможно не надо
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
            return table;
        }

        private static Collection<string> GetDefaultColumnsWithButton()
        {
            return new Collection<string>{
                         /*"InField", 
                         "BaseStationId", 
                         "Latitude", 
                         "Longitude", 
                         "Altitude", 
                         "Azimuth", 
                         "Speed", 
                         "Charge", 
                         "FuelLevel", 
                         "Error", 
                         "ErrorCode", 
                         "SatelliteUsage", 
                         "PacketLossRate", 
                         "Caution",
                         "Emergency",
                         "Notification",
                         "Answer",
                         "PacketError",
                         "NoMotion",
                         "NoGpsSignal",*/
                     };
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
            adapter.TableMappings.Add("Table", tableName);
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
            return;
        }
        
        private void BtnDbConnect_Click(object sender, EventArgs e)
        {
            if (!_dataConfigured)
            {
                if (!Arm_ConfigDataComponents()) return;
                _dataConfigured = true;
                BtnDbConnectSetImage(true);
                FArm_StartTimer();
            }
            else
            {
                _dataConfigured = false;
                BtnDbConnectSetImage(false);
                FArm_StopTimer();
                _dsQuarry.Clear();
            }
            TrackerViewSetColumnWidth();
            TrackerViewSetFilter();
        }
        private void BtnDbConfig_Click(object sender, EventArgs e)
        {
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
                _rModule.StartCommunication();
                _rModuleConnected = true;
                BtnRModuleConnectSetImage(true);
            }
            else
            {
                _rModule.StopCommunication();
                _rModuleConnected = false;
                BtnRModuleConnectSetImage(false);
                ButtonsFieldSetEnabled(null);
            }
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
        private void BtnInField_Click(object sender, EventArgs e)
        {
            if (TrackerView.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Выделено несколько строк! На смену заступит только один"
                    + Environment.NewLine + "Продолжаем?", "Обратите внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (SetInFieldState())
            {
                RefreshDataSetTable(_dsQuarry, _adapters, TrackerView);
                TrackerView_SelectionChanged(TrackerView, e);
            }
        }
        private void BtnNotInField_Click(object sender, EventArgs e)
        {
            if (TrackerView.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Выделено несколько строк! Со смены вернется только один"
                    + Environment.NewLine + "Продолжаем?", "Обратите внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            if (SetInFieldState())
            {
                RefreshDataSetTable(_dsQuarry, _adapters, TrackerView);
                TrackerView_SelectionChanged(TrackerView, e);
            }
        }

        private bool ConnectToRModule()
        {
            var config = Configuration.GetDefault();
            string portName = Properties.Settings.Default.ComPortName;
            
            bool tryConnect = true;
            while (tryConnect)
            {
                if (string.IsNullOrEmpty(portName)) portName = ChoiceComPort();
                if (string.IsNullOrEmpty(portName)) return false;
                if (_rModule==null)
                {
                    _rModule = new RModule(portName, config) {BaudRate = 9600};
                    //var parser = new Parser(new[] { "OK\r", "ERROR\r" }, ModulePak.PakSize, ModulePak.AddressBytesCount);
                    //_rModule.SetParser(parser);
                }
                if (_rModule.IsInit) return true;
                
                tryConnect = false;
                if (!_rModule.InitRModule())
                {
                    MessageBox.Show("Не удается инициализировать радиомодуль на порту " + portName,
                                    "Ошибка инициализации радиомодуля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    portName = string.Empty;
                    tryConnect = true;
                }
            }
            Properties.Settings.Default.ComPortName = portName;
            Properties.Settings.Default.Save();
            _rModule.OnPortError += RModuleOnPortError;
            _rModule.OnDataReceived += RModuleOnDataReceived;
            //_rModule.OnAckReceived += RModuleOnAckReceived;
            //_rModule.OnErrReceived += RModuleOnErrReceived;
            return true;
        }

        private string ChoiceComPort()
        {
            var frm = new ChoisePortDlg(Properties.Settings.Default.ComPortName);
            DialogResult result = frm.ShowDialog(this);
            string resString=string.Empty;
            if (result == DialogResult.OK) resString = frm.FieldPort.Text;
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

        private void RModuleOnErrReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RModuleOnAckReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RModuleOnDataReceived(object sender, EventArgs e)
        {
            _mreHaveData.Set();
        }
        private void RModuleOnPortError(object sender, EventArgs e)
        {
            _rModule.StopCommunication();
            _rModule.Dispose();
            _rModule = null;
            _rModuleConnected = false;
            BtnRModuleConnectSetImage(false);
            ButtonsFieldSetEnabled(null);
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
        private void SetFullFilter(string searchFilter)
        {
            var bs = (BindingSource)TrackerView.DataSource;
            if (bs==null) return;
            searchFilter=searchFilter.Replace("'", "");
            bs.Filter = String.Format("({0} like '%{1}%' or {2} like '%{1}%') and ({3}) ", "_Object", searchFilter, "Code", _filter);
        }

        private bool _dataConfigured;
        private bool _rModuleConnected;
        private readonly DataSet _dsQuarry = new DataSet("QuarryDB");
        private readonly Dictionary<string, SqlDataAdapter> _adapters = new Dictionary<string, SqlDataAdapter>();
        private Timer _timer;
        private string _filter = string.Empty;
        private RModule _rModule;
        private ManualResetEvent _mreHaveData = new ManualResetEvent(true);
        private ManualResetEvent _mreWaitData = new ManualResetEvent(true);

        private bool SetInFieldState()
        {
            
            DataGridView dgv = TrackerView;
            if (dgv == null) return false;
            if (dgv.CurrentRow == null) return false;
            if (dgv.CurrentRow.Index < 0) return false;
            var row = ((DataRowView)dgv.CurrentRow.DataBoundItem).Row;
            int objectId ;
            if (!int.TryParse(row["ObjectId"].ToString(), out objectId)) return false;
            bool inField;
            if (!bool.TryParse(row["InField"].ToString(), out inField)) return false;

            UInt16 status;
            bool sended = GetStatusFromObject(out status, objectId);
            var statusWord = new PakStatusWord(status);
            bool retVal = true;
            if (inField)
            {
                if (sended )
                {
                    
                    if(!statusWord.LampMode) retVal = SetObjectPassiveMode(out status, objectId);
                    if (retVal) retVal = ExecSql_SetObjectInField(objectId, true);
                }
                else
                {
                    if (OperatorRiskAgree())
                    {
                        if (ExecSql_AddEvent(objectId, "Со смены")) retVal = ExecSql_SetObjectInField(objectId, true);
                    }
                }
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                bool stateGood=!(statusWord.Error || statusWord.Charge<9);
                if (sended && stateGood)
                {
                    if(statusWord.LampMode) retVal = SetObjectWorkMode(out status, objectId);
                    if (retVal && !PakStatusWord.Instance(status).LampMode) retVal = ExecSql_SetObjectInField(objectId, false);
                }
                if (retVal && sended && !stateGood) retVal = false;
                Cursor = Cursors.Default;
                if (retVal && !sended)
                {
                    if (OperatorRiskAgree())
                    {
                       if(ExecSql_AddEvent(objectId,"На смену")) retVal = ExecSql_SetObjectInField(objectId, false);
                    }
                }
            }
            return retVal;
        }

        private static bool OperatorRiskAgree()
        {
            return MessageBox.Show("Трекер не доступен. Изменяем режим под вашу ответственность?"
                                   , "Изменение режима объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
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
            var tpReq = new ModulePak(pakReqCoord);
            //_mreWaitData.Reset();
            var queue = (CQueue<ModulePak>)_rModule.InQueue;
            queue.DequeueAll();
            _mreHaveData.Reset();
            _rModule.OutQueueEnq(tpReq);
            bool sign = _mreHaveData.WaitOne(TimeSpan.FromSeconds(10));
            //bool sign = _mreHaveData.WaitOne(10););)
            if (!sign) return false; //Не дождались ответа от трекера
            ModulePak pak;
            for (int i = 0; i < 2; i++)
            {
                while (queue.TryDequeue(out pak))
                {
                    var ps = pak.Structure as PakStructAnsw1;
                    if (ps != null)
                    {
                        if (ps.Adress == adr && ps.IsCrcOk())
                        {
                            status = ps.Status;
                            return true;
                        }
                    }
                }
                Thread.Sleep(waitTime); // Ждем, что пройдут тесты
                //Thread.Sleep(TimeSpan.FromSeconds(1)); 
            }
            
            return false;
        }
        private bool GetStatusFromObject(out UInt16 status, int objectId)
        {
            //return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.RequestCoord);
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.RequestStatus, TimeSpan.FromMilliseconds(500));
        }

        private bool SetObjectPassiveMode(out UInt16 status, int objectId)
        {
            return SendCommandToObject(out status, (UInt16) (objectId), PakCommands.SetModePassive,TimeSpan.FromSeconds(1));
        }

        private bool SetObjectWorkMode(out UInt16 status, int objectId)
        {
            return SendCommandToObject(out status, (UInt16)(objectId), PakCommands.SetModeWork, TimeSpan.FromSeconds(3));
        }

        private static bool ExecSql_SetObjectInField(int objectId, bool currentFieldState)
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
                    MessageBox.Show(
                        "Ошибка выполнения процедуры." + Environment.NewLine + "Текст сообщения:" + ex.Message
                        , "Ошибка выполнения SQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retVal = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
        }
        private static bool ExecSql_AddEvent(int objectId, string value)
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
                    MessageBox.Show(
                        "Ошибка выполнения процедуры." + Environment.NewLine + "Текст сообщения:" + ex.Message
                        , "Ошибка выполнения SQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retVal = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
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

        

        

        

        
    }
}
