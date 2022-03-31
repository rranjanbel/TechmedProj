using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseFeedback
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string Question { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime Datetime { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
