using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    [Keyless]
    public class GetDashboardReportSummaryVM
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
