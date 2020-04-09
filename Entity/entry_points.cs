using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class entry_points : common_field
    {
        [AutoPrimaryKey]
        public int entrypoint_id { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }

        public int category_id { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<int> entrypoint_type_id { get; set; }
        [Range(-90, 90, ErrorMessage = "Invalid latitude")]
        public double lat { get; set; }
        [Range(-180, 180, ErrorMessage = "Invalid longitude")]
        public double lon { get; set; }
        [StringLength(500, ErrorMessage = "Remark name too long (500 character limit).")]
        public string remark { get; set; }
    }
}
