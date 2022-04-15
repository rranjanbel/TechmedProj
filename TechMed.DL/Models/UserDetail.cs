using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TitleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public int GenderId { get; set; }
        public string EmailId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? FatherName { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? Photo { get; set; }
        public string? Occupation { get; set; }
        public bool? IsMarried { get; set; }
        public int? NoOfChildren { get; set; }
        public int? IdproofTypeId { get; set; }
        public string? IdproofNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual CountryMaster? Country { get; set; }
        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual GenderMaster Gender { get; set; } = null!;
        public virtual IdproofTypeMaster? IdproofType { get; set; }
        public virtual StateMaster? State { get; set; }
        public virtual TitleMaster? Title { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual UserMaster User { get; set; } = null!;
    }
}
