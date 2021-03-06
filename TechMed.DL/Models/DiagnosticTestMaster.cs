using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DiagnosticTestMaster
    {
        public DiagnosticTestMaster()
        {
            PatientCaseDiagonosticTests = new HashSet<PatientCaseDiagonosticTest>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatientCaseDiagonosticTest> PatientCaseDiagonosticTests { get; set; }
    }
}
