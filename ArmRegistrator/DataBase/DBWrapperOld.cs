using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace ArmRegistrator.DataBase
{
    [Obsolete]
    internal static class DbWrapperOld
    {
        /// <summary>
        /// Возвращает словарь с перечнем имен столбцов и их типов
        /// </summary>
        /// <param name="ds">Dataset в котором расположена таблица</param>
        /// <param name="tableName">имя таблицы: по которой необходимо сставить словарь</param>
        /// <returns>возвращает null в случае незаданной таблицы или отсутствия столбцов</returns>
        [Obsolete]
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
        [Obsolete]
        private static Dictionary<string, SqlDbType> GetColumnsDictionary(DataTable table)
        {
            if (table==null) return null;

            return table.Columns.Cast<DataColumn>().Where(col => col.ColumnName[0] != '_').ToDictionary(col => col.ColumnName, col => GetDbType(col.DataType));
        }

        private static void HistorySetDateTimes(SqlCommand command, DateTime dBeg, DateTime dEnd)
        {
            command.Parameters["@dateBeg"].Value = dBeg;
            command.Parameters["@dateEnd"].Value = dEnd;
        }

        /// <summary>
        /// Проверяет наличие ошибок в записях
        /// </summary>
        /// <param name="dataSet">проверяемый DataSet</param>
        /// <returns></returns>
        private static bool HaveDataError(DataSet dataSet)
        {
            DataSet ds = dataSet.GetChanges();
            if (ds == null) return false;
            if (!ds.HasErrors) return false;
            return true;
        }

        private static bool HaveRows(DataSet ds, string tableName, string keyField, string value)
        {
            string criteria = string.Format("{0}={1}", keyField, value);

            DataRow[] drs = ds.Tables[tableName].Select(criteria);
            return drs.Length > 0;
        }

        private static bool CreateAndStoreConnectionString()
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

        [Obsolete]
        private static DataConnectionDialog CreateDataConnectionDialog()
        {
            var dialog = new DataConnectionDialog();
            dialog.DataSources.Add(DataSource.SqlDataSource);

            // TODO: русские названия кнопок
            return dialog;
        }

        private static Image ScaleImage(Image source, int width, int height)
        {

            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = (dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = (dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
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
