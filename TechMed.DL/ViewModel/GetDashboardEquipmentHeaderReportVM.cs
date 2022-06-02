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
        public int SrNo { get; set; }
        public int noOfPHC { get; set; }
        public int workingDays { get; set; }
        public Int64 EquipmentAtPHC { get; set; }
        public Int64 ExpectedUpTime { get; set; }
        public Int64 ActualUpTime { get; set; }
        public int Availability { get; set; }           

    }
}
