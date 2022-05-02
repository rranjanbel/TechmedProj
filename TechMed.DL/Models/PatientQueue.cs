using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientQueue
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public int AssignedDoctorId { get; set; }
        public int AssignedBy { get; set; }
        public int CaseFileStatusId { get; set; }
        public DateTime StatusOn { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime AssignedOn { get; set; }

        public virtual Phcmaster AssignedByNavigation { get; set; } = null!;
        public virtual DoctorMaster AssignedDoctor { get; set; } = null!;
        public virtual CaseFileStatusMaster CaseFileStatus { get; set; } = null!;
        public virtual PatientCase PatientCase { get; set; } = null!;
    }
}
