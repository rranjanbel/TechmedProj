using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseVitalsVM
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public int VitalId { get; set; }
        public string VitalName { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public string Value { get; set; } = null!;
        public DateTime? Date { get; set; }
    }
}
