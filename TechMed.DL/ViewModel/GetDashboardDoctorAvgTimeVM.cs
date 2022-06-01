using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardDoctorAvgTimeVM
    {
        public Int64 SrNo { get; set; }
        public string? Specialization { get; set; }
        public string? Doctor { get; set; }
        public string? AvgConsultTime { get; set; }
        public string? CurrentStatus { get; set; }

        //				
    }
}
