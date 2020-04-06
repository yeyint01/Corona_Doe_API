using Entity.shared;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class features : common_field
    {
        [AutoPrimaryKey]
        public long feature_id { get; set; }
        public string type { get; set; }
        public string geometrytype { get; set; }
        public float coordinatesx { get; set; }
        public float coordinatesy { get; set; }
        public long objectid { get; set; }
        [StringLength(500, ErrorMessage = "Name too long (500 character limit).")]
        public string name { get; set; }
        [StringLength(500, ErrorMessage = "Place too long (500 character limit).")]
        public string place { get; set; }
        [StringLength(500, ErrorMessage = "Comment too long (500 character limit).")]
        public string comments { get; set; }
        public float pointx { get; set; }
        public float pointy { get; set; }
        public long fromtime { get; set; }
        public long totime { get; set; }
        public int sourceoid { get; set; }
        public string staytimes { get; set; }
        [StringLength(500, ErrorMessage = "Remark name too long (500 character limit).")]
        public string remark { get; set; }
    }
}
