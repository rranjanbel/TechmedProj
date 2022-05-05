using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class ConsultedPatientByDoctorAndPHCVM
    {
        public long SrNo { get; set; }
        public int NoOfConsultations { get; set; }
        public string Doctor { get; set; }
        public string PHCName { get; set; }

    }
}
