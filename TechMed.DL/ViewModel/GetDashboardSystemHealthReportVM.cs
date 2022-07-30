using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardSystemHealthReportVM
    {
        public Int64 SrNo { get; set; }
        public DateTime date { get; set; }
        public string? WorkingHours { get; set; }
        public int WorkingTime { get; set; }
        public Int64 ServerUpTime { get; set; }
        public Int64 ServerDownTime { get; set; }
        public string? DownTimings { get; set; }
        public int Availability { get; set; }         
    }
}
