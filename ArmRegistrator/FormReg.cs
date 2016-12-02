using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataGridViewExtendedControls.DataGridViewProgress;
using Microsoft.Data.ConnectionUI;

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
            SetConnectionButtonImage(false);
            CreateTrackerViewColumns();
            CreateCardViewColumns();
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
            var columns = GetDefaultTrackerColumnTitles();
            CreateDataGridViewColumn(dgv, columns);
            TrackerViewAddProgressColumns();
            TrackerViewAddCheckBoxColumns();
        }
        private static void CreateDataGridViewColumn(DataGridView dgv, Dictionary<string ,string> columns)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn lastColumn=null;
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
        private void SetConnectionButtonImage(bool isConnected)
        {
            BtnConnect.Image = isConnected ? Properties.Resources.ImageConnectionActive : Properties.Resources.ImageConnectionDeactive;
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
                                                     
                                                 };
        }
        private static Dictionary<string,string> GetDefaultCardColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"Title", "Параметр"},
                                                     {"Value", "Значение"},
                                                 };
        }
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!_dataConfigured)
            {
                if (!Arm_ConfigDataComponents()) return;
                _dataConfigured = true;
                SetConnectionButtonImage(true);
                FArm_StartTimer();
            }
            else
            {
                _dataConfigured = false;
                SetConnectionButtonImage(false);
                FArm_StopTimer();
                _dsQuarry.Clear();
            }
            TrackerViewSetColumnWidth();
            TrackerViewSetFilter();
        }
        private void TrackerViewSetColumnWidth()
        {
            DataGridView dgv = TrackerView;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.Width = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
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
        private void TrackerViewSetFilter()
        {
            DataGridView dgv = TrackerView;
            BindingSource bs = (BindingSource) dgv.DataSource;
            if (bs==null) return;
            StringBuilder sbType = new StringBuilder(" or 1<>1");
            if (FlagPersonal.Checked) sbType.Append(" or ObjectTypeId=1");
            if (FlagTransport.Checked) sbType.Append(" or ObjectTypeId=2");
            if (FlagTechnics.Checked) sbType.Append(" or ObjectTypeId=3");
            if (sbType.Length>0)
            {
                sbType.Replace(" or ", "", 0, 5);
                sbType.Insert(0, "(");
                sbType.Append(")");
            }

            StringBuilder sbField = new StringBuilder(" or 1<>1");
            if (FlagIsInField.Checked) sbField.Append(" or InField=1");
            if (FlagIsNotInField.Checked) sbField.Append(" or InField=0");
            if (FlagLongTimeInField.Checked) sbField.Append(" or LongTimeInField=1");
            if (sbField.Length > 0)
            {
                sbField.Replace(" or ", "", 0, 5);
               // sbField.Insert(0, "(");
                //sbField.Append(")");
            }
            sbType.AppendFormat("and({0})", sbField);
            bs.Filter = sbType.ToString();
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
                DataRow dr = dataSet.Tables[pair.Key].Select("", string.Format("{0} desc", pair.Value))[0];
                int tmp;
                int.TryParse(dr[pair.Value].ToString(), out tmp);
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

        private void BtnConnectDbConfig_Click(object sender, EventArgs e)
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

        private void TrackerView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dgvCard = CardView;
            if (dgv.CurrentRow == null)
            {
                SetCommandButtonEnabled(null);
                dgvCard.DataSource = null;
                return;
            }
            if (dgv.CurrentRow.Index < 0) return;
            if (_lastRow == dgv.CurrentRow) return;
//            if (_dsQuarry.Tables["ObjectT"] == null) return;
            _lastRow = dgv.CurrentRow;
            
            //var cellAdress = dgvCard.CurrentCellAddress;
            var row = ((DataRowView) dgv.CurrentRow.DataBoundItem).Row;
            SetCommandButtonEnabled((bool)row["InField"]);

            var objectType = (int)row["ObjectTypeId"];

            DataTable table = _dsQuarry.Tables["ObjectVT"];
            
            if (objectType==1)
            {
                table = _dsQuarry.Tables["ObjectET"];
            }
            if (table==null) return;
            if (dgvCard.DataSource == null) dgvCard.DataSource = table;
            else if (!dgvCard.DataSource.Equals(table)) dgvCard.DataSource = table;
            TransposedTableRefresh(((DataRowView)dgv.CurrentRow.DataBoundItem).Row, table);
            //SelectDataGridViewCell(dgvCard, cellAdress.Y, cellAdress.X);
        }
        private void SetCommandButtonEnabled(object commandEnable)
        {
            if (commandEnable==null)
            {
                BtnInField.Enabled = false;
                BtnNotInField.Enabled = false;
                return;
            }
            var enableBtn = (bool) commandEnable;
            BtnInField.Enabled = !enableBtn;
            BtnNotInField.Enabled = enableBtn;
        }
        private bool _dataConfigured;
        private readonly DataSet _dsQuarry = new DataSet("QuarryDB");
        private readonly Dictionary<string, SqlDataAdapter> _adapters = new Dictionary<string, SqlDataAdapter>();
        private Timer _timer;
        private DataGridViewRow _lastRow = new DataGridViewRow();

        private void FlagPersonal_Click(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }

        private void BtnAllObjectType_Click(object sender, EventArgs e)
        {
            FlagPersonal.Checked = true;
            FlagTransport.Checked = true;
            FlagTechnics.Checked = true;
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

        private void BtnAllField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = true;
            FlagIsNotInField.Checked = true;
            FlagLongTimeInField.Checked = false;
            TrackerViewSetFilter();
        }

        private void FlagIsInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            TrackerViewSetFilter();
        }

        private void FlagIsNotInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            TrackerViewSetFilter();
        }

        private void FlagLongTimeInField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = false;
            FlagIsNotInField.Checked = false;
            TrackerViewSetFilter();
        }
    }
}
