using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PHCPatientCount
    {
        public long? ID { get; set; } = 0;
        public string? PHCName { get; set; }=null;
        public int? TotalPatients { get; set; } = 0;
        public int? TotalConsulted { get; set; } = 0;
        public int? TotalPending { get; set; } = 0;

    }
}
