using System;

namespace DataAccess.shared
{
    public class Functions
    {
        public static int PageCount(int rows)
        {
            return (int)Math.Ceiling((decimal)rows / (decimal)Variables.RowsInPage);
        }

        public static string GetStayTimeString(long from, long to)
        {
            var startDt = new DateTime(from);
            var endDt = new DateTime(to);

            return startDt.ToString("HH:mm") + "-" + endDt.ToString("HH:mm");
        }
    }
}
