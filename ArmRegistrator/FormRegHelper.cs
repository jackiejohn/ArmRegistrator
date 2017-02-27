using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArmRegistrator
{
    class FormRegHelper
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
            return new Collection<string>
            {
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
    }
}
