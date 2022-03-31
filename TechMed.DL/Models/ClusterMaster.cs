using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class ClusterMaster
    {
        public ClusterMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
            Phcmasters = new HashSet<Phcmaster>();
            ZoneMasters = new HashSet<ZoneMaster>();
        }

        public int Id { get; set; }
        public string Cluster { get; set; } = null!;
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<Phcmaster> Phcmasters { get; set; }
        public virtual ICollection<ZoneMaster> ZoneMasters { get; set; }
    }
}
