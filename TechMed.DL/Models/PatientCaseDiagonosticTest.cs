using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class PatientCaseDiagonosticTest
    {
        public long Id { get; set; }
        public long PatientCaseID { get; set; }
        public int DiagonosticTestID { get; set; }      
        public DateTime CreatedOn { get; set; }

        public virtual PatientCase PatientCase { get; set; } = null!;
        public virtual DiagnosticTestMaster DiagnosticTest { get; set; } = null!;
    }
}
