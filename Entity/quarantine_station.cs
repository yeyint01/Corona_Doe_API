using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class quarantine_station : common_field
    {
        [AutoPrimaryKey]
        public int station_id { get; set; }
        [Required(ErrorMessage = "Please fill myanmar name.")]
        [StringLength(500, ErrorMessage = "Myanmar name too long (500 character limit).")]
        public string name_mm { get; set; }
        [Required(ErrorMessage = "Please fill english name.")]
        [StringLength(500, ErrorMessage = "English name too long (500 character limit).")]
        public string name_en { get; set; }       
        [StringLength(500, ErrorMessage = "Description too long (500 character limit).")]
        public string description { get; set; }
        public int? location_id { get; set; }
        public int? capacity { get; set; }
        public bool? discontinued { get; set; }
        public DateTime? discontinued_date { get; set; }

    }
}
