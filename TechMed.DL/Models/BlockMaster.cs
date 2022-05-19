using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class BlockMaster
    {
        public BlockMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
            Phcmasters = new HashSet<Phcmaster>();
        }

        public int Id { get; set; }
        public string BlockName { get; set; } = null!;
        public int DistrictId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual DistrictMaster District { get; set; } = null!;
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<Phcmaster> Phcmasters { get; set; }
    }
}
