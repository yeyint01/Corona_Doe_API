using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class region : common_field
    {
        public int region_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }
    }
}
