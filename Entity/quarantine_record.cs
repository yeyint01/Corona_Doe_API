using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class quarantine_record : common_field
    {
        [AutoPrimaryKey]
        public int quarantine_id { get; set; }
        public int? station_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        [Required(ErrorMessage = "Please fill name.")]
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
        [StringLength(500, ErrorMessage = "Travel from too long (500 character limit).")]
        public string traveled_from { get; set; }
        [StringLength(500, ErrorMessage = "Fever history too long (500 character limit).")]
        public string fever_history { get; set; }
        [StringLength(500, ErrorMessage = "Remark too long (500 character limit).")]
        public string remark { get; set; }
        public DateTime? checkout_date { get; set; }
        [StringLength(50, ErrorMessage = "Result too long (50 character limit).")]
        public string result { get; set; }
        public bool lab_testing { get; set; }
        public DateTime? lab_testing_date { get; set; }
        public DateTime? result_date { get; set; }
        public bool? checkedout { get; set; }

    }
}

