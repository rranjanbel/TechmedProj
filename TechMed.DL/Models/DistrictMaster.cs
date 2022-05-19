using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DistrictMaster
    {
        public DistrictMaster()
        {
            BlockMasters = new HashSet<BlockMaster>();
            DoctorMasters = new HashSet<DoctorMaster>();
            PatientMasters = new HashSet<PatientMaster>();
            Phcmasters = new HashSet<Phcmaster>();
        }

        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int StateId { get; set; }
        public string DistrictName { get; set; } = null!;

        public virtual DivisionMaster Division { get; set; } = null!;
        public virtual StateMaster State { get; set; } = null!;
        public virtual ICollection<BlockMaster> BlockMasters { get; set; }
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
        public virtual ICollection<Phcmaster> Phcmasters { get; set; }
    }
}
