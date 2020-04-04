using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class entry_reason : common_field
    {
        [AutoPrimaryKey]
        public int reason_id { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }       
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        [StringLength(500, ErrorMessage = "Description too long (500 character limit).")]
        public string description { get; set; }

    }
}
