using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class quarantine_record : common_field
    {
        [AutoPrimaryKey]
        public int quarantine_id { get; set; }
        public int? station_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string person_name { get; set; }
        public string person_nrc { get; set; }
        public string person_ph { get; set; }
        public string person_age { get; set; }
        public DateTime? person_dob { get; set; }
        public string gender { get; set; }
        public string hometown { get; set; }
        public int? reason_id { get; set; }
        public string travel_history { get; set; }
        public string residence_address { get; set; }
        public string current_address { get; set; }
        public string traveled_from { get; set; }
        public string fever_history { get; set; }
        public string remark { get; set; }
        public DateTime? checkout_date { get; set; }
        public string result { get; set; }
        public bool? checkedout { get; set; }

    }
}

