using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PHCPatientCount
    {
        public int ID { get; set; }
        public string PHCName { get; set; }
        public int TotalPatients { get; set; }
        public int TotalConsulted { get; set; }
        public int TotalPending { get; set; }

    }
}
