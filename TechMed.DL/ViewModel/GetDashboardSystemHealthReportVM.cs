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
        public string WorkingTime { get; set; }
        public string ServerUpTime { get; set; }
        public string ServerDownTime { get; set; }

        public long WorkingTimeSS { get; set; }
        public long ServerUpTimeSS { get; set; }
        public long ServerDownTimeSS { get; set; }

        public string? DownTimings { get; set; }
        public int Availability { get; set; }         
    }
}
