using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class region : common_field
    {
        [AutoPrimaryKey]
        public int region_id { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name is too long (500 character limited).")]
        public string name_mm { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name is too long (500 character limited).")]
        public string name_en { get; set; }       
    }
}
