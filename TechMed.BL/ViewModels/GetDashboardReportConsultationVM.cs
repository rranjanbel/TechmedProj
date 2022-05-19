using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class GetDashboardReportConsultationVM
    {
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? OPDNo { get; set; }
        public int PHCID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
