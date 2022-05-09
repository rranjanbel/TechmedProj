using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientMaster
    {
        public PatientMaster()
        {
            PatientCases = new HashSet<PatientCase>();
            VideoCallTransactions = new HashSet<VideoCallTransaction>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public long PatientId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int IdproofId { get; set; }
        public string IdproofNumber { get; set; } = null!;
        public int GenderId { get; set; }
        public string? Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string? City { get; set; }
        public string PinCode { get; set; } = null!;
        public string? Photo { get; set; }
        public DateTime Dob { get; set; }
        public string? EmailId { get; set; }
        public string MobileNo { get; set; } = null!;
        public int PatientStatusId { get; set; }
        public int Phcid { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MaritalStatusID { get; set; }

        public virtual CountryMaster Country { get; set; } = null!;
        public virtual Phcmaster? CreatedByNavigation { get; set; }
        public virtual DistrictMaster District { get; set; } = null!;
        public virtual GenderMaster Gender { get; set; } = null!;
        public virtual IdproofTypeMaster Idproof { get; set; } = null!;
        public virtual PatientStatusMaster PatientStatus { get; set; } = null!;
        public virtual Phcmaster Phc { get; set; } = null!;
        public virtual StateMaster State { get; set; } = null!;
        public virtual Phcmaster? UpdatedByNavigation { get; set; }
        public virtual ICollection<PatientCase> PatientCases { get; set; }
        public virtual ICollection<VideoCallTransaction> VideoCallTransactions { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; } = null!;
    }
}
