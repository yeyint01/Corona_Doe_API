using System;
using System.Collections.Generic;

namespace Entity
{
    public class trace_param
    {
        public string mhash { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }

    public class trace_result
    {
        public IEnumerable<target_route> target_routes { get; set; }
        public IEnumerable<contact_person> contact_people { get; set; }
    }

    public class target_route
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class contact_person
    {
        public string mhash { get; set; }
        public int count { get; set; }
        public DateTime last_contact { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
