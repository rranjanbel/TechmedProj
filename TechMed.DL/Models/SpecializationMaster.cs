using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class SpecializationMaster
    {
        public SpecializationMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
            SubSpecializationMasters = new HashSet<SubSpecializationMaster>();
        }

        public int Id { get; set; }
        public string Specialization { get; set; } = null!;

        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<SubSpecializationMaster> SubSpecializationMasters { get; set; }
    }
}
