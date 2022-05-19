using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class IdproofTypeMaster
    {
        public IdproofTypeMaster()
        {
            PatientMasters = new HashSet<PatientMaster>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string IdproofType { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
