﻿using Entity.shared;
using System.Collections.Generic;

namespace Entity
{
    public class featuresParam : SortInfo
    {
        public string Name { get; set; }
        public int PgNo { get; set; }
        public bool IsMobile { get; set; }
    }

    public class featuresResult : PageInfo
    {
        public IEnumerable<patient_history> Features { get; set; }
    }
}
