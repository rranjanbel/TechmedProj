using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DistrictMaster
    {
        public DistrictMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
            PatientMasters = new HashSet<PatientMaster>();
        }

        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int StateId { get; set; }
        public string DistrictName { get; set; } = null!;

        public virtual DivisionMaster Division { get; set; } = null!;
        public virtual StateMaster State { get; set; } = null!;
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
    }
}
