using System.Data.Common;

namespace ArticlesApi.Helpers
{
    public class CheckReader
    {
        public static T GetValue<T>(DbDataReader reader, string dbColumn, T orderField)
        {
            if (!(reader[dbColumn] is DBNull))
            {
                return (T)Convert.ChangeType(reader[dbColumn], typeof(T));
            }
            return default(T);
        }

        public static T GetValue<T>(DbDataReader reader, string dbColumn)
        {
            if (!(reader[dbColumn] is DBNull))
            {
                return (T)Convert.ChangeType(reader[dbColumn], typeof(T));
            }
            return default(T);
        }
        public static Nullable<T> GetValueWithNull<T>(DbDataReader reader, string dbColumn) where T : struct
        {
            if (!(reader[dbColumn] is DBNull))
            {
                return (T)Convert.ChangeType(reader[dbColumn], typeof(T));
            }
            return null;
        }
    }
}
