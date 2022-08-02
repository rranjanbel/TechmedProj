using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class AddDoctorDTO
    {
        [Required]
        public int BlockId { get; set; }
        [Required]
        public int ClusterId { get; set; }
        [Required]
        public int SpecializationId { get; set; }
        public int? SubSpecializationId { get; set; }
        [Required]
        public string Mciid { get; set; } = null!;
        [Required]
        public string RegistrationNumber { get; set; } = null!;
        [Required]
        public string Qualification { get; set; } = null!;
        [Required]
        public string Designation { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string DigitalSignature { get; set; } = null!;
        //[Required]
        public string? PanNo { get; set; } = null!;
        //[Required]
        public string? BankName { get; set; } = null!;
        //[Required]
        public string? BranchName { get; set; } = null!;
        //[Required]
        public string? AccountNumber { get; set; } = null!;
        //[Required]
        public string? Ifsccode { get; set; } = null!;
        [Required]
        public int CreatedBy { get; set; }
        public AddDoctorDetaisDTO detailsDTO { get; set; } = null!;
    }
    public class AddDoctorDetaisDTO
    {
        [Required]
        public int TitleId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        //[Required]
        public DateTime Dob { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string EmailId { get; set; } = null!;
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string? PinCode { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string Photo { get; set; } = null!;
        //[Required]
        public int? IdproofTypeId { get; set; }
        //[Required]
        public string? IdproofNumber { get; set; } = null!;
    }
}
