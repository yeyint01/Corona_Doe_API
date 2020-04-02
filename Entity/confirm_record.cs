using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class confirm_record : common_field
    {
        [AutoPrimaryKey]
        public int patient_id { get; set; }
        public int? quarantine_id { get; set; }
        public int? station_id { get; set; }
        public string patient_name { get; set;  }
        public string patient_nrc { get; set; }
        public string patient_ph { get; set; }
        public string patient_age { get; set; }
        public DateTime? patient_dob { get; set; }
        public string gender { get; set; }
        public string hometown { get; set; }
        public int? reason_id { get; set; }
        public string travel_history { get; set; }
        public string residence_address { get; set; }
        public string current_address { get; set; }
        public string traveled_from { get; set; }
        public string fever_history { get; set; }
        public string remark { get; set; }
        public DateTime? result_date { get; set; }
        public string result { get; set; }

    }
}
