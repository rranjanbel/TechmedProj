using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PHCLoginHistoryReportVM
    {
        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string Zone { get; set; }
        public string PHCName { get; set; }
        public int UserID { get; set; }
        public DateTime LogedDate { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
        public string TotalTime { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
    }
}
