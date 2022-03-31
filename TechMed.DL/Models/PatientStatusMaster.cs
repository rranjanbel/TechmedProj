using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientStatusMaster
    {
        public PatientStatusMaster()
        {
            PatientMasters = new HashSet<PatientMaster>();
        }

        public int Id { get; set; }
        public string PatientStatus { get; set; } = null!;

        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
    }
}
