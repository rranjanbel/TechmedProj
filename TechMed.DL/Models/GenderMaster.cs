using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class GenderMaster
    {
        public GenderMaster()
        {
            PatientMasters = new HashSet<PatientMaster>();
            UserDetails = new HashSet<UserDetail>();
            AgeGroupMaster = new HashSet<AgeGroupMaster>();
        }

        public int Id { get; set; }
        public string Gender { get; set; } = null!;

        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<AgeGroupMaster> AgeGroupMaster { get; set; }
    }
}
