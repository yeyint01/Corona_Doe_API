using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class entry_record : common_field
    {
        [AutoPrimaryKey]
        public int id { get; set; }
        public int? entrypoint_id { get; set; }
        public DateTime? entrance_date { get; set; }
        [Required(ErrorMessage = "Please fill full name.")]
        [StringLength(500, ErrorMessage = "Name too long (500 character limit).")]
        public string person_name { get; set; }
        [StringLength(50, ErrorMessage = "Nrc too long (50 character limit).")]
        public string person_nrc { get; set; }
        [Required(ErrorMessage = "Please fill phone number.")]
        [PhoneNumber(ErrorMessage = "Phone number invalid.")]
        [StringLength(50, ErrorMessage = "Phone number too long (50 character limit).")]
        public string person_ph { get; set; }
        [StringLength(50, ErrorMessage = "Age too long (50 character limit).")]
        public string person_age { get; set; }
        public DateTime? person_dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender too long (50 character limit).")]
        public string gender { get; set; }
        [StringLength(50, ErrorMessage = "Hometown too long (50 character limit).")]
        public string hometown { get; set; }
        public int? reason_id { get; set; }
        [StringLength(500, ErrorMessage = "Travel history too long (500 character limit).")]
        public string travel_history { get; set; }
        [StringLength(500, ErrorMessage = "Residence address too long (500 character limit).")]
        public string residence_address { get; set; }
        [StringLength(500, ErrorMessage = "Current address too long (500 character limit).")]
        public string current_address { get; set; }
        [StringLength(500, ErrorMessage = "Traveled from too long (500 character limit).")]
        public string traveled_from { get; set; }
        [StringLength(500, ErrorMessage = "Fever history too long (500 character limit).")]
        public string fever_history { get; set; }        
        [StringLength(50, ErrorMessage = "Personal ID too long (50 character limit).")]
        public string personal_id { get; set; }        
        [StringLength(500, ErrorMessage = "Remark too long (500 character limit).")]
        public int? location_id { get; set; }
        public string remark { get; set; }

    }
}
