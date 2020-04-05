using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class entrypoint_categoryParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class entrypoint_categoryResult : PageInfo
    {
        public IEnumerable<entrypoint_category> Entrypoint_Categories { get; set; }
    }
}
