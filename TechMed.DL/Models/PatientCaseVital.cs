using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseVital
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public int VitalId { get; set; }
        public string Value { get; set; } = null!;
        public DateTime? Date { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
        public virtual VitalMaster Vital { get; set; } = null!;
    }
}
