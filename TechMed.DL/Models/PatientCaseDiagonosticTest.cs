using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCaseDiagonosticTest
    {
        public int Id { get; set; }
        public int DiagonosticTestId { get; set; }
        public long PatientCaseId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual DiagnosticTestMaster DiagonosticTest { get; set; } = null!;
        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
