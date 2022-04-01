using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class VitalMasterDTO
    {
        public int Id { get; set; }
        public string Vital { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
