using System;

namespace Entity.shared
{
    public class GlobalClass
    {
        public static DateTime? GetLocalDateTime(DateTime? param)
        {
            DateTime? result = null;

            if (param == null)
                result = null;
            else if (((DateTime)param).Kind == DateTimeKind.Utc)
                result = ((DateTime)param).ToLocalTime();
            else
                result = param;

            return result;
        }

        public static DateTime GetLocalDateTime(DateTime param)
        {
            DateTime result = DateTime.Now;

            if (param == null)
                result = DateTime.Now;
            else if (param.Kind == DateTimeKind.Utc)
                result = param.ToLocalTime();
            else
                result = param;

            return result;
        }
    }
}
