using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class IdproofTypeMaster
    {
        public IdproofTypeMaster()
        {
            DoctorMasters = new HashSet<DoctorMaster>();
            PatientMasters = new HashSet<PatientMaster>();
            Phcmasters = new HashSet<Phcmaster>();
        }

        public int Id { get; set; }
        public string IdproofType { get; set; } = null!;

        public virtual ICollection<DoctorMaster> DoctorMasters { get; set; }
        public virtual ICollection<PatientMaster> PatientMasters { get; set; }
        public virtual ICollection<Phcmaster> Phcmasters { get; set; }
    }
}
