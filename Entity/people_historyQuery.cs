using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class people_historyParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }        
    }

    public class people_historyResult : PageInfo
    {
        public IEnumerable<people_history> People_Histories { get; set; }
    }
}
