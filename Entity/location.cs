using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class location : common_field
    {
        [AutoPrimaryKey]
        public int location_id { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        public int? region_id { get; set; }
        public int? locationtype_id { get; set; }
    }
}
