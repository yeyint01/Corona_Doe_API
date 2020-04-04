using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class locationParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class locationResult : PageInfo
    {
        public IEnumerable<location> Locations { get; set; }
    }
}
