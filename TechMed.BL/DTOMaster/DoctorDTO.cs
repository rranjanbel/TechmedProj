using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class DoctorDTO
    {
        public DoctorDTO()
        {
            detailsDTO=new DetailsDTO();
        }
        public int Id { get; set; }
        [Required]
        public int? BlockID { get; set; }
        [Required]
        public int? ClusterId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SpecializationId { get; set; }
        public string? Specialization { get; set; }
        public int? SubSpecializationId { get; set; }
        public string? SubSpecialization { get; set; }
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
        //[Required]
        public string DigitalSignature { get; set; } = null!;

        //[Required]
        public string DigitalSignatureNewUpdate { get; set; } = null!;
        //[Required]
        public string PanNo { get; set; } = null!;
        //[Required]
        public string BankName { get; set; } = null!;
        //[Required]
        public string BranchName { get; set; } = null!;
        //[Required]
        public string AccountNumber { get; set; } = null!;
        //[Required]
        public string Ifsccode { get; set; } = null!;
        [Required]
        public int? UpdatedBy { get; set; }
        public DetailsDTO detailsDTO { get; set; } = null!;
    }
    public class DetailsDTO
    {
        [Required]
        public int? TitleId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        //[Required]
        public DateTime? Dob { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string? Gender { get; set; }
        [Required]
        public string EmailId { get; set; } = null!;
        [Required]
        public int? CountryId { get; set; }
        [Required]
        public int? StateId { get; set; }
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string? PinCode { get; set; }
        public string Photo { get; set; } = null!;
        public string PhotoNewUpdate { get; set; } = null!;
        //[Required]
        public int? IdproofTypeId { get; set; }
        //[Required]
        public string IdproofNumber { get; set; } = null!;
        public string? Address { get; set; }
    }
}
