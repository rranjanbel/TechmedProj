using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }


        public int TitleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public int GenderId { get; set; }
        public string EmailId { get; set; } = null!;
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string City { get; set; } = null!;


        public int SpecializationId { get; set; }
        public int? SubSpecializationId { get; set; }
        public string Mciid { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public string Qualification { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public byte[]? DigitalSignature { get; set; } = null!;
        public string PanNo { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string Ifsccode { get; set; } = null!;
        public int IdproofTypeId { get; set; }
        public string IdproofNumber { get; set; } = null!;
        //public int? CreatedBy { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }
    }
}
