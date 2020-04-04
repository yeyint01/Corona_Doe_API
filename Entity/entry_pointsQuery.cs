using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class entry_pointsParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }
    public class entry_pointsResult : PageInfo
    {
        public IEnumerable<entry_points> Entry_Points { get; set; }
    }
}
