using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Phcmaster
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }
        public string Occupation { get; set; } = null!;
        public bool IsMarried { get; set; }
        public int NoOfChildren { get; set; }
        public int IdproofTypeId { get; set; }
        public string IdproofNumber { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ClusterMaster Cluster { get; set; } = null!;
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual IdproofTypeMaster IdproofType { get; set; } = null!;
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual UserMaster User { get; set; } = null!;
        public virtual ZoneMaster Zone { get; set; } = null!;
    }
}
