using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PHCManpowerVM
    {
        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string PHCName { get; set; }
        public int NoOfDaysInMonth { get; set; }
        public int WorkingDays { get; set; }
        public int DaysPresent { get; set; }
        public int DaysAbsent { get; set; }
        public int PHCID { get; set; }
       
          
    }
}
