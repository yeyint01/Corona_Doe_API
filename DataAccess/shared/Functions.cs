using System;

namespace DataAccess.shared
{
    public class Functions
    {
        public static int PageCount(int rows)
        {
            return (int)Math.Ceiling((decimal)rows / (decimal)Variables.RowsInPage);
        }
    }
}
