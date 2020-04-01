using Entity.shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace DataAccess.shared
{
    public class DbAccess
    {
        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(Variables.ConnString);

        public static string SelectAll<T>()
        {
            return "SELECT * FROM " + typeof(T).Name;
        }

        public static string Select<T>()
        {
            return "SELECT * FROM " + typeof(T).Name + " WHERE " + GetPKColumns<T>();
        }

        public static string Insert<T>()
        {
            return "INSERT INTO " + typeof(T).Name + "(" + GetColumns<T>(Coltype.insert) + ") VALUES(" + GetColumns<T>(Coltype.insertv) + ")";
        }

        public static string InsertAutoId<T>()
        {
            return "INSERT INTO " + typeof(T).Name + "(" + GetColumns<T>(Coltype.insert) + ") VALUES(" + GetColumns<T>(Coltype.insertv) + ") SELECT SCOPE_IDENTITY()";
        }

        public static string Update<T>()
        {
            return "UPDATE " + typeof(T).Name + " SET " + GetColumns<T>(Coltype.update) + " WHERE " + GetPKColumns<T>();
        }

        public static string Delete<T>()
        {
            return "DELETE FROM " + typeof(T).Name + " WHERE " + GetPKColumns<T>();
        }

        #region Reflection Helper

        enum Coltype
        {
            insert, // col
            insertv, // @col
            update // col=@col
        }

        private static string GetColumns<T>(Coltype type)
        {
            string result = "";

            foreach (var pInfo in typeof(T).GetProperties())
            {
                if (IsAutoPKColumn(pInfo) || IsIgnoreColumn(pInfo)) continue;

                switch (type)
                {
                    case Coltype.insert:
                        result += pInfo.Name + ",";
                        break;
                    case Coltype.insertv:
                        result += "@" + pInfo.Name + ",";
                        break;
                    case Coltype.update:
                        if (IsUpdateIgnoreColumn(pInfo))
                            continue;
                        result += pInfo.Name + "=@" + pInfo.Name + ",";
                        break;
                }
            }

            // remove last ','
            if (result.Length > 1)
                result = result.Remove(result.Length - 1);

            return result;
        }

        private static string GetPKColumns<T>()
        {
            string result = "";
            foreach (var pInfo in typeof(T).GetProperties())
            {
                if (!IsAutoPKColumn(pInfo) && !IsPKColumn(pInfo)) continue;

                result += pInfo.Name + "=@" + pInfo.Name + " AND ";
            }

            if (result.Length > 0)
                result = result.Remove(result.LastIndexOf(" AND "));

            return result;
        }

        private static bool IsAutoPKColumn(PropertyInfo pInfo)
        {
            var atts = pInfo.GetCustomAttributes(typeof(AutoPrimaryKeyAttribute), true);

            return atts.Length > 0;
        }

        private static bool IsPKColumn(PropertyInfo pInfo)
        {
            var atts = pInfo.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);

            return atts.Length > 0;
        }

        private static bool IsIgnoreColumn(PropertyInfo pInfo)
        {
            var atts = pInfo.GetCustomAttributes(typeof(IgnoreAttribute), true);

            return atts.Length > 0;
        }

        private static bool IsUpdateIgnoreColumn(PropertyInfo pInfo)
        {
            var atts = pInfo.GetCustomAttributes(typeof(UpdateIgnoreAttribute), true);

            return atts.Length > 0;
        }

        //private static bool IsValidColumn(PropertyInfo pInfo)
        //{
        //    return pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.IsValueType || pInfo.PropertyType == typeof(string);
        //}

        #endregion
    }
}
