using Entity.shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class location_type
    {
        [AutoPrimaryKey]
        public int locationtype_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }

    }
}
