using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class confirm_record : common_field
    {
        [AutoPrimaryKey]
        public int patient_id { get; set; }
        public Nullable<int> quarantine_id { get; set; }
        public Nullable<int> station_id { get; set; }
        [Required(ErrorMessage = "Please fill patient name.")]
        [StringLength(500, ErrorMessage = "Patient name too long (500 character limit).")]
        public string patient_name { get; set;  }       
        [StringLength(50, ErrorMessage = "Nrc too long (50 character limit).")]
        public string patient_nrc { get; set; }
        [Required(ErrorMessage = "Please fill phone number.")]
        [PhoneNumber(ErrorMessage = "Phone number invalid.")]
        [StringLength(50, ErrorMessage = "Phone number too long (50 character limit).")]
        public string patient_ph { get; set; }
        [StringLength(50, ErrorMessage = "Age too long (50 character limit).")]
        public string patient_age { get; set; }
        public DateTime? patient_dob { get; set; }
        [StringLength(50, ErrorMessage = "Gender too long (50 character limit).")]
        public string gender { get; set; }
        [StringLength(50, ErrorMessage = "Hometown too long (50 character limit).")]
        public string hometown { get; set; }
        public Nullable<int> reason_id { get; set; }
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
        [StringLength(500, ErrorMessage = "Remark too long (500 character limit).")]
        public string remark { get; set; }
        public DateTime? result_date { get; set; }
        [StringLength(50, ErrorMessage = "Result too long (50 character limit).")]
        public string result { get; set; }

    }
}
