using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardEquipmentHeaderReportVM
    {
        public Int64 SrNo { get; set; }
        public int noOfPHC { get; set; }
        public int workingDays { get; set; }
        public int EquipmentAtPHC { get; set; }
        public string ExpectedUpTime { get; set; }
        public string ActualUpTime { get; set; }
        public int Availability { get; set; }           

    }
}
