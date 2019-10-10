using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ArmRegistrator.DataBase
{
    public class DbWrapper
    {
        public DbWrapper(int refreshTime, string connectionString)
        {
            _timerInterval = refreshTime * 1000;
            _connectionString = connectionString;
        }

        public bool PrepareDataSetObjects(int refreshTime, string connectionString)
        {
            
            _timerInterval = refreshTime * 1000;
            _connectionString = connectionString;
            _isPrepared = false;

            var sqlb = new SqlConnectionStringBuilder(_connectionString);
            var conStr = sqlb.ToString();
            if (string.IsNullOrEmpty(conStr))
            {
                MessageBox.Show("Не определена строка подключения", "Ошибка записи в БД", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            _adapters.Clear();
            var conn = new SqlConnection(conStr);
            
            try
            {
                conn.Open();
                DataSetHelper.ConfigDataCreateDataAdapter(_dsQuarry, conn, "Object", "pa_ArmRegistrSelect", new KeyValuePair<string, SqlDbType>("ObjectId", SqlDbType.Int), _adapters);
                DataSetHelper.SetPrimaryKeyOnTable(_dsQuarry, "Object", "ObjectId");
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
            DataSetHelper.ConfigDataAutoIncrementColumns(_dsQuarry, dict);

            var vehicleColumns = new[]
                                      {
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["_Number"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["VehicleTypeName"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["FuelLevelMax"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["ObjectId"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Code"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["ServiceName"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Chief"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Phone"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["InFieldTime"],
                                          
                                      };
            Dictionary<string, string> columnTitles = FormHelper.GetDefaultTrackerColumnTitles();
            DataSetHelper.CreateTransposedTable(_dsQuarry, vehicleColumns, string.Format("{0}VT", OBJECT_TABLE_NAME), columnTitles);
            var employeeColumns = new[]
                                      {
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Surname"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Name"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Patronymic"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Position"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["_Number"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["ObjectId"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Code"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["ServiceName"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Chief"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["Phone"],
                                          _dsQuarry.Tables[OBJECT_TABLE_NAME].Columns["InFieldTime"],
                                      };
            DataSetHelper.CreateTransposedTable(_dsQuarry, employeeColumns, string.Format("{0}ET", OBJECT_TABLE_NAME), columnTitles);
            _isPrepared = true;
            return _isPrepared;
        }

        public void RefreshObjectTable()
        {
            RefreshDataSetTables(_dsQuarry, _adapters, new[] { "Object" });
        }
        public void RefreshDataSetAllTables()
        {
            RefreshDataSetTables(_dsQuarry, _adapters, null);
        }

        public DataSet Data
        {
            get { return _dsQuarry; }
        }
        public void ClearDataSet()
        {
            _dsQuarry.Clear();
        }
        public DataTable DataTableForObjectType( int objectType)
        {
            var tableName = objectType == 1 ? "ObjectET" : "ObjectVT";

            if (!_dsQuarry.Tables.Contains(tableName)) return null;

            return _dsQuarry.Tables[tableName];
        }

        

        public void TimerStart()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Tick += Timer_Tick;
            }
            if (_timerInterval == 0) return;
            _timer.Interval = _timerInterval;
            _timer.Start();
        }
        public void TimerStop()
        {
            if (_timer != null) _timer.Stop();
        }

        public bool WriteObjectInFieldState(bool addEvent, int objectId, bool currentInField)
        {
            bool retVal = true;
            
            if (addEvent)
            {
                var eventText = currentInField ? "Со смены" : "На смену";
                retVal = ExecSql_AddEvent(objectId, eventText);
            }
            if (retVal) retVal = ExecSql_SetObjectInField(objectId, currentInField);
            return retVal;
        }
        public void WriteAddedReplacePair(int objectId, int activeObjectId)
        {
            ExecSql_AddRepleacePair(objectId, activeObjectId);
        }
        public void WriteDeletedReplacePair(int objectId)
        {
            ExecSql_DelRepleacePair(objectId);
        }

        public ObjectRecordValue ReadObjectId(string rfidLabel)
        {
            if (string.IsNullOrEmpty(rfidLabel)) return new ObjectRecordValue();
            return ExecSql_ReadObjectIdByRfid(rfidLabel);
        }

        public PersonalData ReadEmployeeData(int objectId)
        {
            var bsObj = new BindingSource(_dsQuarry, OBJECT_TABLE_NAME) { Filter = string.Format("ObjectId={0}", objectId) };

            Image imgTmp = Properties.Resources.NoEmployeeImage2;
            var persData = new PersonalData();
            if (bsObj.Count != 1) return persData;

            DataRow row = ((DataRowView)(bsObj.Current)).Row;
            if (!Convert.IsDBNull(row["Photo"]))
            {
                using (var ms = new MemoryStream())
                {
                    ms.Write((byte[])row["Photo"], 0, ((byte[])row["Photo"]).Length);
                    imgTmp = Image.FromStream(ms);
                }
            }
            persData.Photo = imgTmp;
            persData.ObjectId = objectId;
            persData.Fio= string.Format("{0} {1} {2}",row["Surname"], row["Name"],row["Patronymic"]);
            persData.Dolj = row["Position"].ToString();
            return persData;
        }

        public const string OBJECT_TABLE_NAME = "Object";


        private void RefreshDataSetTables(DataSet dataSet, Dictionary<string, SqlDataAdapter> adapters, IEnumerable<string> tblNames)
        {
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

        private bool TimerIsStarted()
        {
            if (_timer == null) return false;
            return _timer.Enabled;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RefreshDataSetTables(_dsQuarry, _adapters, null);
        }

        private bool ExecSql_SetObjectInField(int objectId, bool currentFieldState)
        {
            var cmd = new SqlCommand {CommandType = CommandType.StoredProcedure, CommandText = "p_SetObjectInField"};
            cmd.Parameters.AddWithValue("@ObjectId", objectId);
            cmd.Parameters.AddWithValue("@InField", !currentFieldState);

            return ExecSqlNonQuery(cmd);
        }
        private bool ExecSql_AddEvent(int objectId, string value)
        {
            var cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = "p_AddEvent" };
            cmd.Parameters.AddWithValue("@EventTypeId", 1);
            cmd.Parameters.AddWithValue("@Message", "Изменение режима объекта без непосредственного контроля");
            cmd.Parameters.AddWithValue("@Source", "АРМ-Р");
            cmd.Parameters.AddWithValue("@ObjectId", objectId);
            cmd.Parameters.AddWithValue("@Value", value);

            return ExecSqlNonQuery(cmd);
        }

        private void ExecSql_AddRepleacePair(int objectId, int activeObjectId)
        {
            var cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = "pa_ArmRegistrAddPair" };
            cmd.Parameters.AddWithValue("@ObjectId", objectId);
            cmd.Parameters.AddWithValue("@ActiveObjectId", activeObjectId);
            ExecSqlNonQuery(cmd);
            return;
        }
        private void ExecSql_DelRepleacePair(int objectId)
        {
            var cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = "pa_ArmRegistrDelPair" };
            cmd.Parameters.AddWithValue("@ObjectId", objectId);
            ExecSqlNonQuery(cmd);
            return;
        }

        private ObjectRecordValue ExecSql_ReadObjectIdByRfid(string labelString)
        {
            var cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = "pa_ArmRegistrGetObjectIdByRfid" };
            cmd.Parameters.AddWithValue("@label", labelString);
            var inField=cmd.Parameters.Add("@inField", SqlDbType.Bit);
            inField.Direction = ParameterDirection.Output;
            var activeObjectId = cmd.Parameters.Add("@ActiveObjectId", SqlDbType.SmallInt);
            activeObjectId.Direction = ParameterDirection.Output;
            var retParam=cmd.Parameters.Add("@ObjectId", SqlDbType.BigInt);
            retParam.Direction = ParameterDirection.ReturnValue;
            var returnValue = new ObjectRecordValue();
            if (!ExecSqlNonQuery(cmd)) return returnValue;
            
            returnValue.ObjectId = (int) retParam.Value;
            returnValue.InField = (bool)inField.Value;
            returnValue.ActiveObjectId = (short)activeObjectId.Value;

            return returnValue;
        }

        private bool ExecSqlNonQuery(SqlCommand command)
        {
            var timerState = TimerIsStarted();

            if (!_isPrepared)
            {
                TimerStop();
                MessageBox.Show("Подключение к БД не подготовлено"
                        , "Ошибка выполнения SQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (timerState) TimerStart();
                return false;
            }
            bool isExecuted = true;
            using (var connection = new SqlConnection(_connectionString))
            {
                var sbError = new StringBuilder();
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch (SqlException exSql)
                {
                    sbError.AppendFormat("Ошибка SQL: {0}", exSql.Message);
                }
                catch (Exception ex)
                {
                    sbError.AppendFormat("Ошибка: {0}", ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                if (sbError.Length>0)
                {
                    TimerStop();
                    MessageBox.Show(sbError.ToString(), "Ошибка записи в БД", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    if (timerState) TimerStart();
                    isExecuted = false;
                }
            }
            return isExecuted;
        }
       

        private readonly DataSet _dsQuarry = new DataSet("QuarryDB");
        private readonly Dictionary<string, SqlDataAdapter> _adapters = new Dictionary<string, SqlDataAdapter>();
        private Timer _timer;
        private int _timerInterval;
        private string _connectionString;
        private bool _isPrepared;


        public struct ObjectRecordValue
        {
            internal int ObjectId;
            internal bool InField;
            internal short ActiveObjectId;
        }

        public struct PersonalData
        {
            internal string Fio;
            internal string Dolj;
            internal Image Photo;
            internal int ObjectId;
        }

        
    }
}
