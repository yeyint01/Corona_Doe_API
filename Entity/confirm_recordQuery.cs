using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class confirm_recordParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }
    public class confirm_recordResult : PageInfo
    {
        public IEnumerable<confirm_record> Confirm_Records { get; set; }
    }
}
  

