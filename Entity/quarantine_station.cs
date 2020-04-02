using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class quarantine_station : common_field
    {
        [AutoPrimaryKey]
        public int station_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }
        public string description { get; set; }
        public Nullable<int> location_id { get; set; }
        public int capacity { get; set; }
        public bool discontinued { get; set; }
        public DateTime discontinued_date { get; set; }

    }
}
