using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientMasterDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public long PatientId { get; set; }
        public string PhoneNumber { get; set; }       
        public int IdproofId { get; set; }       
        public string IdproofNumber { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string? Address { get; set; }      
        public int CountryId { get; set; }        
        public int StateId { get; set; }
        public string? State { get; set; }
        public int DistrictId { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string PinCode { get; set; }
        public string? Photo { get; set; }
        public DateTime Dob { get; set; }
        public string EmailId { get; set; }        
        public string MobileNo { get; set; }
        public int Phcid { get; set; }
        public int PatientStatusId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Age { get; set; }
        public int MaritalStatusID { get; set; }
        public string? GuardianName { get; set; }
    }
}
