using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class people_history
    {
        [PrimaryKey]
        [StringLength(100, ErrorMessage = "Mobile ID too long (100 character limit).")]
        public string mid { get; set; }
        [PrimaryKey]
        [StringLength(100, ErrorMessage = "Device ID too long (100 character limit).")]
        public string did { get; set; }
        [PrimaryKey]
        [StringLength(100, ErrorMessage = "Mobile hash too long (100 character limit).")]
        public string mhash { get; set; }
        [PrimaryKey]
        [StringLength(100, ErrorMessage = "Device hash too long (100 character limit).")]
        public string dhash { get; set; }
        public DateTime timestamp { get; set; }
        public decimal duration { get; set; }
        [Range(-90, 90, ErrorMessage = "Invalid latitude")]
        public float lat { get; set; }
        [Range(-180, 180, ErrorMessage = "Invalid longitude")]
        public float lng { get; set; }
        [StringLength(50, ErrorMessage = "Source too long (50 character limit).")]
        public string source { get; set; }
        [StringLength(50, ErrorMessage = "Source too long (50 character limit).")]
        public string eventname { get; set; }
        [StringLength(100, ErrorMessage = "Location too long (100 character limit).")]
        public string location { get; set; }
        [StringLength(100, ErrorMessage = "Contact too long (100 character limit).")]
        public string contact { get; set; }
        [StringLength(50, ErrorMessage = "Contact type too long (50 character limit).")]
        public string contacttype { get; set; }
        [StringLength(100, ErrorMessage = "Remark too long (100 character limit).")]
        public string remark { get; set; }       
    }
}
