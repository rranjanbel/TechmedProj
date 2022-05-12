using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class DashboardReportSummaryVM
    {
        public long SLNo { get; set; }
        public string? District { get; set; }
        public string? Block { get; set; }
        public string? PHC { get; set; }
        public int Total { get; set; }
        public int GeneralPractice { get; set; }
        public int ObstetricsAndGyne { get; set; }
        public int Pediatrics { get; set; }
    }
}
