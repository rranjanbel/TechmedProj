using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseMedicine
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string Medicine { get; set; } = null!;
        public string? Dose { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
