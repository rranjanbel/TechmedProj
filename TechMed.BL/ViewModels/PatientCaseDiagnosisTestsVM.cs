using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseDiagnosisTestsVM
    {
        public int Id { get; set; }
        public long PatientCaseID { get; set; }
        public int DiagonosticTestID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DiagonosticTestName { get; set; }
    }
}
