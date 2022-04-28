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
        public string? Observation { get; set; }
        public string? Symptom { get; set; }
        public string? Allergies { get; set; }
        public string? FamilyHistory { get; set; }
        public string? Diagnosis { get; set; }
        public string? Instruction { get; set; }
        public string? Prescription { get; set; }
        public string? Test { get; set; }
        public int CreatedBy { get; set; }     
        public int? UpdatedBy { get; set; }       
    }
}
