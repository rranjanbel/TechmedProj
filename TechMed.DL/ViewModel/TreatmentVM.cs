using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;


namespace TechMed.DL.ViewModel
{
    public class TreatmentVM
    {
        public TreatmentVM()
        {
            medicineVMs = new List<MedicineVM>();

            PatientCaseDiagonostics = new List<PatientCaseDiagonisticTestVM>();
        }
        [Required]
        public long PatientCaseID { get; set; } 
        public string Instruction { get; set; }     
        public string? Findings { get; set; }        
        public string? Prescription { get; set; }
        public string? SuggestedDiagnosis { get; set; }
        public string? ProvisionalDiagnosis { get; set; }
        public string? ReferredTo { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Comment { get; set; }
        public List<MedicineVM> medicineVMs { get; set; }
        public List<PatientCaseDiagonisticTestVM> PatientCaseDiagonostics { get; set; }
       public string Observation { get; set; }
      
    }
    public class MedicineVM
    {
        [Required]
        public int DrugID { get; set; }
        [Required]
        public bool Morning { get; set; }
        [Required]
        public bool Noon { get; set; }
        [Required]
        public bool Night { get; set; }
        [Required]
        public bool EmptyStomach { get; set; }
        [Required]
        public bool AfterMeal { get; set; }
        [Required]
        public bool OD { get; set; }
        [Required]
        public bool BD { get; set; }
        [Required]
        public bool TD { get; set; }
        public int Duration { get; set; } = 0;

    }

    public class PatientCaseDiagonisticTestVM
    {
        public long Id { get; set; }
        public long PatientCaseID { get; set; }
        public int DiagonosticTestID { get; set; }       
    }
}
