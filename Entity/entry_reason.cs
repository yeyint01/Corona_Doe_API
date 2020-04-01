using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class entry_reason : common_field
    {
        public int reason_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }
        public string description { get; set; }

    }
}
