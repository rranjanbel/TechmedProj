using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseDocument
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
        public string? Description { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
