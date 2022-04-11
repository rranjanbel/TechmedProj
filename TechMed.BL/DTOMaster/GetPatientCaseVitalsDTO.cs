using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetPatientCaseVitalsDTO
    {
        public string Vital { get; set; } = null!;
        public string Value { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime? Date { get; set; }

    }
}
