using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Phcmaster
    {
        public Phcmaster()
        {
            EmployeeTrainings = new HashSet<EmployeeTraining>();
            EquipmentUptimeReports = new HashSet<EquipmentUptimeReport>();
            PatientMasterCreatedByNavigations = new HashSet<PatientMaster>();
            PatientMasterPhcs = new HashSet<PatientMaster>();
            PatientMasterUpdatedByNavigations = new HashSet<PatientMaster>();
            PatientQueues = new HashSet<PatientQueue>();
        }

        public int Id { get; set; }
        public int ClusterId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int UserId { get; set; }
        public string Phcname { get; set; } = null!;
        public string MailId { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public string? Address { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? EmployeeName { get; set; }

        public virtual BlockMaster Block { get; set; } = null!;
        public virtual ClusterMaster Cluster { get; set; } = null!;
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual DistrictMaster District { get; set; } = null!;
        public virtual DivisionMaster Division { get; set; } = null!;
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual UserMaster User { get; set; } = null!;
        public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
        public virtual ICollection<EquipmentUptimeReport> EquipmentUptimeReports { get; set; }
        public virtual ICollection<PatientMaster> PatientMasterCreatedByNavigations { get; set; }
        public virtual ICollection<PatientMaster> PatientMasterPhcs { get; set; }
        public virtual ICollection<PatientMaster> PatientMasterUpdatedByNavigations { get; set; }
        public virtual ICollection<PatientQueue> PatientQueues { get; set; }
    }
}
