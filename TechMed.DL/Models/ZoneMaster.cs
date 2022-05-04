using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class ZoneMaster
    {
        public ZoneMaster()
        {
            DistrictMasters = new HashSet<DistrictMaster>();
            DoctorMasters = new HashSet<DoctorMaster>();
            Phcmasters = new HashSet<Phcmaster>();
        }

        public int Id { get; set; }
        public int ClusterId { get; set; }
        public string Zone { get; set; } = null!;
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ClusterMaster Cluster { get; set; } = null!;
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual ICollection<DistrictMaster> DistrictMasters { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<Phcmaster> Phcmasters { get; set; }
    }
}
