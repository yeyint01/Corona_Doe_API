using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class user_accountParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
        public bool IsMobile { get; set; }
    }

    public class user_accountResult : PageInfo
    {
        public IEnumerable<user_account> User_Accounts { get; set; }
    }
}
