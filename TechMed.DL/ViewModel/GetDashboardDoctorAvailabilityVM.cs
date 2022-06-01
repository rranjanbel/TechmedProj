using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardDoctorAvailabilityVM
    {
        public Int64 SrNo { get; set; }
        public string? Specialization { get; set; }
        public string? Doctor { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? LogedInTime { get; set; }
        public DateTime? FirstConsultTime { get; set; }
        public DateTime? LastConsultTime { get; set; }
        public DateTime? LogedoutTime { get; set; }
        public int NoOfConsultation { get; set; }
    }
}
