using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Phcmaster
    {
        public Phcmaster()
        {
            PatientMasters = new HashSet<PatientMaster>();
        }

        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }
        public string Phcname { get; set; } = null!;
        public string MailId { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public string? Address { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ClusterMaster Cluster { get; set; } = null!;
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual UserMaster User { get; set; } = null!;
        public virtual ZoneMaster Zone { get; set; } = null!;
        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
    }
}
