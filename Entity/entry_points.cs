using System;
using Entity.shared;

namespace Entity
{
    public class entry_points : common_field
    {
        [AutoPrimaryKey]
        public int entrypoint_id { get; set; }
        public string name_en { get; set; }
        public string name_mm { get; set; }
        public int location_id { get; set; }
        public int entrypoint_type_id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string remark { get; set; }

    }
}
