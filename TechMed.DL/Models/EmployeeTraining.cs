using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class EmployeeTraining
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

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual Phcmaster Phc { get; set; } = null!;
        public virtual UserMaster? UpdatedByNavigation { get; set; }
    }
}
