using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class quarantine_stationParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
    }

    public class quarantine_stationResult : PageInfo
    {
        public IEnumerable<quarantine_station> Quarantine_Stations { get; set; }
    }
}
