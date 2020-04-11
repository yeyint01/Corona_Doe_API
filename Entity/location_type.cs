using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class location_type
    {
        [AutoPrimaryKey]
        public int locationtype_id { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }             
        public int? region_id { get; set; }
        [StringLength(50, ErrorMessage = "Township code too long (50 character limit).")]
        public string tcode { get; set; }
        [Range(-90, 90, ErrorMessage = "Invalid latitude")]
        public float lat { get; set; }
        [Range(-180, 180, ErrorMessage = "Invalid longitude")]
        public float lng { get; set; }
    }
}
