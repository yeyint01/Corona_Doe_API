using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class quarantine_recordParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }

        public bool IsMobile { get; set; }
    }

    public class quarantine_recordResult : PageInfo
    {
        public IEnumerable<quarantine_record> Quarantine_Records { get; set; }
    }
}
