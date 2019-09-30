using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewExtendedControls.DataGridViewProgress;

namespace ArmRegistrator
{
    class FormHelper
    {
        public static Dictionary<string, string> GetDefaultTrackerColumnTitles()
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
        public static Dictionary<string, string> GetVisibleTrackerColumnNames()
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
                newDic.Add(columnName, desc);
            }
            return newDic;
        }
        public static Dictionary<string, string> GetDefaultCardColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"Title", "Параметр"},
                                                     {"Value", "Значение"},
                                                 };
        }
        public static Collection<string> GetDefaultColumnsWithButton()
        {
            return new Collection<string>()
            /*{
                "InField", 
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
                "NoGpsSignal",
            }*/
            ;
        }
        public static DataGridViewProgressCell GetDefaultProgressCell()
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

        public static bool OperatorRiskAgree()
        {
            return MessageBox.Show("Трекер не доступен. Изменяем режим под вашу ответственность?"
                                   , "Изменение режима объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                   DialogResult.Yes;
        }

        public static bool OperatorRiskAgreeModem()
        {
            return MessageBox.Show("АРМ прекращает использование радиомодема."
                + Environment.NewLine + "Изменения режимов будут проводиться под вашей ответственностью."
                + Environment.NewLine + "Продолжаем?"
                                   , "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                   DialogResult.Yes;
        }

        public static void InvokeButtonSetImage(Button ctrl, Bitmap newImage)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new Action<Bitmap>(img => { ctrl.Image = img; }), newImage);
            }
            else
            {
                ctrl.Image = newImage;
            }
        }
        public static void InvokeLableSetText(Label ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new Action<string>(txt => { ctrl.Text = txt; }), text);
            }
            else
            {
                ctrl.Text = text;
            }
        }
        public static void InvokePictureBoxSetImage(PictureBox ctrl, Image picture)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new Action<Image>(img => { ctrl.Image = img; }), picture);
            }
            else
            {
                ctrl.Image = picture;
            }
        }
    }
}
