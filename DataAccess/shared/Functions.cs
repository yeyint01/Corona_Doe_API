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
            var startDt = MillisecondsToDate(from);
            var endDt = MillisecondsToDate(to);

            return startDt.ToString("HH:mm") + "-" + endDt.ToString("HH:mm");
        }

        public static long DateToMilliseconds(DateTime dt)
        {
            return (long)dt.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static DateTime MillisecondsToDate(long ms)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(ms);
            DateTime startdate = new DateTime(1970, 1, 1) + time;

            return startdate;
        }
    }
}
