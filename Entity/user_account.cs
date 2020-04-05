using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class user_account : common_field
    {
        [AutoPrimaryKey]
        public int user_id { get; set; }
        [Required(ErrorMessage = "Please fill user name.")]
        [StringLength(500, ErrorMessage = "User name too long (50 character limit).")]
        public string user_name { get; set; }       
        [Password]
        [StringLength(200, ErrorMessage = "Password too long (200 character limit).")]
        public string password { get; set; }
        [Required(ErrorMessage = "Please fill full name.")]
        [StringLength(200, ErrorMessage = "Name too long (200 character limit).")]
        public string full_name { get; set; }
        [Required(ErrorMessage = "Please fill phone number.")]
        [PhoneNumber(ErrorMessage = "Phone number invalid.")]
        [StringLength(50, ErrorMessage = "Phone number too long (50 character limit).")]
        public string phone_no { get; set; }
        public bool dashboard_user { get; set; }
        public bool entrypoint_user { get; set; }
        public int? entrypoint_id { get; set; }
        public bool point_admin_right { get; set; }
        public bool quarantine_user { get; set; }
        public int? station_id { get; set; }
        public bool active { get; set; }
        public string remark { get; set; }     
    }
}
