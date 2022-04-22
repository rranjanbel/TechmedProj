using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientFeedbackDTO
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        //public string Question { get; set; } = null!;
        public int Rating { get; set; }
        //public DateTime Datetime { get; set; }
    }
}
