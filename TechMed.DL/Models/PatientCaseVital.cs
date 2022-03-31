using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseVital
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string Vital { get; set; } = null!;
        public decimal Value { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime? Date { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
