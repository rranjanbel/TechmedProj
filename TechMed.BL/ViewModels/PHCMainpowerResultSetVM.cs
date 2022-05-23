using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.ViewModel;

namespace TechMed.BL.ViewModels
{
    public class PHCMainpowerResultSetVM
    {
        public List<PHCManpowerVM> PHCManpowerReports { get; set; }
        public int NoOfDaysInMonth { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalPresentDays { get; set; }
        public decimal AvailabilityPercentage { get; set; }
    }
}
