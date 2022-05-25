using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class EmployeeTrainingDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string TrainingSubject { get; set; } = null!;
        public string TraingPeriod { get; set; } = null!;
        public DateTime TraingDate { get; set; }
        public string TrainingBy { get; set; } = null!;
        public int EmployeeFeedback { get; set; }
        public int PhcId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
