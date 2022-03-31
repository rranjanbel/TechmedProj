using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class CaseFileStatusMaster
    {
        public CaseFileStatusMaster()
        {
            PatientQueues = new HashSet<PatientQueue>();
        }

        public int Id { get; set; }
        public string FileStatus { get; set; } = null!;

        public virtual ICollection<PatientQueue> PatientQueues { get; set; }
    }
}
