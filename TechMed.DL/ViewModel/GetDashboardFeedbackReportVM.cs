using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardFeedbackReportVM
    {
        public Int64 SrNo { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? PHCName { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? DoctorName { get; set; }
        public int Feedback { get; set; }
        public string? Comments { get; set; }  
    }
}
