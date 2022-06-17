using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class RemoteSiteDowntimeSummaryMonthlyVM
    {
        public Int64 SrNo { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? PHCName { get; set; }
        public int TotalWorkingTime { get; set; }
        public int PHCDownTime { get; set; }
        public int DownTime { get; set; }
    }
}
