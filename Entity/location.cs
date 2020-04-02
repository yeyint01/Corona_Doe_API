using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class location : common_field
    {
        [AutoPrimaryKey]
        public int location_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }
        public int? region_id { get; set; }
        public int? locationtype_id { get; set; }
    }
}
