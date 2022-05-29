using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DiagnosticTestMaster
    {
        public DiagnosticTestMaster()
        {
            PatientCaseDiagonostics = new HashSet<PatientCaseDiagonosticTest>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatientCaseDiagonosticTest> PatientCaseDiagonostics { get; set; }
    }
}
