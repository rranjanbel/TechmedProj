using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class SubSpecializationMaster
    {
        public SubSpecializationMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
        }

        public int Id { get; set; }
        public int SpecializationId { get; set; }
        public string SubSpecialization { get; set; } = null!;

        public virtual SpecializationMaster Specialization { get; set; } = null!;
        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
    }
}
