using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class location_typeParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class location_typeResult : PageInfo
    {
        public IEnumerable<location_type> location_Types { get; set; }
    }
}
