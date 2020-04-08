using System;
using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class people_history
    {
        [PrimaryKey]
        [Required(ErrorMessage = "Please fill phone number or ID.")]
        [StringLength(100, ErrorMessage = "Phone number or ID too long (100 character limit).")]
        public string ph_or_id { get; set; }
        [PrimaryKey]
        public DateTime visited_at { get; set; }
        public decimal? duration { get; set; }
        [Range(-90, 90, ErrorMessage = "Invalid latitude")]
        public double lat { get; set; }
        [Range(-180, 180, ErrorMessage = "Invalid longitude")]
        public double lon { get; set; }
        [Required(ErrorMessage = "Please fill source ID.")]
        [StringLength(50, ErrorMessage = "Source ID too long (50 character limit).")]
        public string source_id { get; set; }
    }
}
