using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ArmRegistrator.DataBase
{
    class DataSetHelper
    {
        public static void CreateTransposedTable(DataSet ds, IEnumerable<DataColumn> columns, string transTableName, Dictionary<string, string> columnsTitle)
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

            var buttonCollection = FormHelper.GetDefaultColumnsWithButton();
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

        public static void SetPrimaryKeyOnTable(DataSet dataSet, string tableName, string keyCol)
        {
            dataSet.Tables[tableName].PrimaryKey = new[] { dataSet.Tables[tableName].Columns[keyCol] };
        }
        
        public static void ConfigDataCreateDataAdapter(DataSet dataSet, SqlConnection conn, string tableName, string selCommand, KeyValuePair<string, SqlDbType> keyCol, Dictionary<string, SqlDataAdapter> adapters)
        {
            SqlDataAdapter adp = CreateDataAdapter(dataSet, conn, tableName, selCommand, keyCol);
            if (adp != null) adapters.Add(tableName, adp);
        }

        public static void ConfigDataAutoIncrementColumns(DataSet dataSet, Dictionary<string, string> tableKeyDict)
        {

            foreach (KeyValuePair<string, string> pair in tableKeyDict)
            {
                DataRow[] drs = dataSet.Tables[pair.Key].Select("", string.Format("{0} desc", pair.Value));
                int tmp = 0;
                if (drs.Length > 0)
                {
                    DataRow dr = drs[0];
                    int.TryParse(dr[pair.Value].ToString(), out tmp);
                }

                DataColumn dt = dataSet.Tables[pair.Key].Columns[pair.Value];
                dt.AutoIncrement = true;
                dt.AutoIncrementSeed = tmp + 1;
            }
        }

        public static void TransposedTableRefresh(DataRow row, DataTable transTable)
        {
            if (transTable == null) return;
            foreach (DataRow dataRow in transTable.Rows)
            {
                var name = (string)dataRow["Name"];
                dataRow["Value"] = row[name];
            }
            var rows = transTable.Select("Name='InFieldTime'");
            if (rows.Length == 0) return;
            DataRow transRow = rows[0];
            DateTime newTime;
            var ci = new CultureInfo("en-US");
            var parsed = DateTime.TryParse(transRow["Value"].ToString(), ci, DateTimeStyles.AssumeUniversal, out newTime);
            if (parsed) transRow["Value"] = newTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            return;
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

            Dictionary<string, SqlDbType> cols = GetColumnsDictionary(dataSet, tableName);

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
        private static void AddParams(SqlCommand cmd, Dictionary<string, SqlDbType> cols)
        {
            foreach (KeyValuePair<string, SqlDbType> col in cols)
            {
                cmd.Parameters.Add("@" + col.Key, col.Value, 0, col.Key);
            }
        }

        /// <summary>
        /// Возвращает словарь с перечнем имен столбцов и их типов
        /// </summary>
        /// <param name="ds">Dataset в котором расположена таблица</param>
        /// <param name="tableName">имя таблицы: по которой необходимо сставить словарь</param>
        /// <returns>возвращает null в случае незаданной таблицы или отсутствия столбцов</returns>
        private static Dictionary<string, SqlDbType> GetColumnsDictionary(DataSet ds, string tableName)
        {
            if (!ds.Tables.Contains(tableName)) return null;

            DataTable table = ds.Tables[tableName];
            return GetColumnsDictionary(table);
        }

        /// <summary>
        /// Возвращает словарь с перечнем имен столбцов и их типов
        /// </summary>
        /// <param name="table">Таблица: на основании которой формируется словарь</param>
        /// <returns>возвращает null в случае незаданной таблицы или отсутствия столбцов</returns>
        private static Dictionary<string, SqlDbType> GetColumnsDictionary(DataTable table)
        {
            if (table == null) return null;

            return table.Columns.Cast<DataColumn>().Where(col => col.ColumnName[0] != '_').ToDictionary(col => col.ColumnName, col => GetDbType(col.DataType));
        }
        private static SqlDbType GetDbType(Type theType)
        {
            var p1 = new SqlParameter();
            var tc = TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
            {
                p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
            }
            else
            {
                //Try brute force
                try
                {
                    p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
                }
                catch (Exception)
                {
                    if (theType.Name == "Byte[]") p1.DbType = DbType.Binary;// (DbType)SqlDbType.VarBinary;
                    //Do Nothing; will return NVarChar as default
                }
            }
            return p1.SqlDbType;
        }
        
    }
}
