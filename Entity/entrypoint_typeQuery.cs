using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class entrypoint_typeParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }
    public class entrypoint_typeResult : PageInfo
    {
        public IEnumerable<entrypoint_type> Entrypoint_Types { get; set; }
    }
}
