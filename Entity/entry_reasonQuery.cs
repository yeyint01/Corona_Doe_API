using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class entry_reasonParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class entry_reasonResult : PageInfo
    {
        public IEnumerable<entry_reason> Entry_Reasons { get; set; }
    }
}
