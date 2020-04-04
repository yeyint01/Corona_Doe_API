using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class entry_recordParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class entry_recordResult : PageInfo
    {
        public IEnumerable<entry_record> Entry_Records { get; set; }
    }
}
