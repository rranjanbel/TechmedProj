using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientCaseDTO
    {
        public long ID { get; set; }
        public int PatientId { get; set; }
        public string CaseFileNumber { get; set; } = null!;
        public string CaseHeading { get; set; } = null!;  
        public string? Symptom { get; set; }
        public string? Observation { get; set; }
        public string? Allergies { get; set; }
        public string? FamilyHistory { get; set; }
        public string? SuggestedDiagnosis { get; set; }
        public string? ProvisionalDiagnosis { get; set; }
        public string? ReferredTo { get; set; }
        public string? Instruction { get; set; }       
        public string? Finding { get; set; }     
        public string? Prescription { get; set; }
        public int CreatedBy { get; set; }     
        public int? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }       
        public string? Opdno { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int SpecializationID { get; set; }
        public int? CaseStatusID { get; set; }
        public string? Comment { get; set; } = null!;
    }
}
