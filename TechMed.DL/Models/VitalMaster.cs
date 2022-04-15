using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class VitalMaster
    {
        public VitalMaster()
        {
            PatientCaseVitals = new HashSet<PatientCaseVital>();
        }

        public int Id { get; set; }
        public string Vital { get; set; } = null!;
        public string Unit { get; set; } = null!;

        public virtual ICollection<PatientCaseVital> PatientCaseVitals { get; set; }
    }
}
