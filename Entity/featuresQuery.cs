using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class featuresParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class featuresResult : PageInfo
    {
        public IEnumerable<features> Features { get; set; }
    }
}
