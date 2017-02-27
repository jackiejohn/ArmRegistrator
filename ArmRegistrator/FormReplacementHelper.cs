using System.Collections.Generic;

namespace ArmRegistrator
{
    static class  FormReplacementHelper
    {
        public static Dictionary<string, string> GetDefaultObjectColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"ObjectId", "ID"},
                                                     {"Code", "Маркер"},
                                                     {"_Object", "Объект"},
                                                     {"InField", "На смене"},
                                                     {"_ActiveObjectId", "Действующий ID"},
                                                     {"_ReplDate", "Дата подмены"},
                                                 };
        }

        public static Dictionary<string, string> GetDefaultMayReplacementColumnTitles()
        {
            return new Dictionary<string, string>
                                                 {
                                                     {"ObjectId", "ID"},
                                                     {"Code", "Маркер"},
                                                     {"_Object", "Объект"},
                                                     {"InField", "На смене"},
                                                     {"_PassiveObjectId", "Пассивный ID"},
                                                     {"_PassiveReplDate", "Дата подмены"},
                                                 };
        }
        public static IEnumerable<string> GetCheckBoxColumnNames()
        {
            return new[] {"InField"};
        }

        
    }
}
