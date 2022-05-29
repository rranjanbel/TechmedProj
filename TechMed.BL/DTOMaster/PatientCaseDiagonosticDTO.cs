using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientCaseDiagonosticDTO
    {
        public long Id { get; set; }
        public long PatientCaseID { get; set; }
        public int DiagonosticTestID { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
