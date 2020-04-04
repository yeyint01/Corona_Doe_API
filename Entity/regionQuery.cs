using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class regionParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class regionResult : PageInfo
    {
        public IEnumerable<region> Regions { get; set; }
    }
}
