using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardAppointmentVM
    {
        public long SrNo { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? PHCName { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? Doctor { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public string? ConsultStatus { get; set; }
        public string? DoctorAvailable { get; set; }
        public string? PatientAvailable { get; set; }
                  
    }
}
