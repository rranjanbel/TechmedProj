using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardEmployeeFeedbackVM
    {

        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string PHCName { get; set; }
        public string EmployeeName { get; set; }
        public string TrainingSubject { get; set; }
        public string TrainingBy { get; set; }
        public DateTime TraingDate { get; set; }
        public int EmployeeFeedback { get; set; }
     
    }
}
