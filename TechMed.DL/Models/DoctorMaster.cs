using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DoctorMaster
    {
        public DoctorMaster()
        {
            PatientQueues = new HashSet<PatientQueue>();
        }

        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }
        public int SpecializationId { get; set; }
        public int? SubSpecializationId { get; set; }
        public string Mciid { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public string Qualification { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DigitalSignature { get; set; } = null!;
        public string Panno { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string Ifsccode { get; set; } = null!;
        public bool IsOnline { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ClusterMaster Cluster { get; set; } = null!;
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual SpecializationMaster Specialization { get; set; } = null!;
        public virtual SubSpecializationMaster? SubSpecialization { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual UserMaster User { get; set; } = null!;
        public virtual ZoneMaster Zone { get; set; } = null!;
        public virtual ICollection<PatientQueue> PatientQueues { get; set; }
    }
}
