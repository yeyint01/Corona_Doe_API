using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class entrypoint_type : common_field
    {
        [AutoPrimaryKey]
        public int entrypoint_type_id { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }       
        [StringLength(500, ErrorMessage = "Remark too long (500 character limit).")]
        public string remark { get; set; }
        
    }
}
