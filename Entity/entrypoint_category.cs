using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class entrypoint_category : common_field
    {
        [AutoPrimaryKey]
        public int category_id { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }       
    }
}
