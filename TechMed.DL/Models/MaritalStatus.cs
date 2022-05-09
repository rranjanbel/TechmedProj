using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            PatientMasters = new HashSet<PatientMaster>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
    }
}
