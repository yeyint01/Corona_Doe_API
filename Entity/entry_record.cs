using System;
using System.Collections.Generic;
using System.Text;
using Entity.shared;

namespace Entity
{
    public class entry_record : common_field
    {
        [AutoPrimaryKey]
        public int id { get; set; }
        public int? entrypoint_id { get; set; }
        public DateTime? entrance_date { get; set; }
        public int? location_id { get; set; }
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

    }
}
