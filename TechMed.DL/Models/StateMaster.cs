using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class StateMaster
    {
        public StateMaster()
        {
            DistrictMasters = new HashSet<DistrictMaster>();
            PatientMasters = new HashSet<PatientMaster>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string StateName { get; set; } = null!;

        public virtual ICollection<DistrictMaster> DistrictMasters { get; set; }
        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
