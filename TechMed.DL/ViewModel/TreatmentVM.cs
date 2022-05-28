using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class TreatmentVM
    {
        [Required]
        public long PatientCaseID { get; set; }    
        //[Required]
        public string Instruction { get; set; }
        //[Required]
        public string Test { get; set; }
        [Required]
        public string Findings { get; set; }
        [Required]
        public string Prescription { get; set; }
        public string? SuggestedDiagnosis { get; set; }
        public string? ProvisionalDiagnosis { get; set; }
        public string? ReferredTo { get; set; }

        public List<MedicineVM> medicineVMs { get; set; }
       
        public TreatmentVM()
        {
            medicineVMs = new List<MedicineVM>();
        }
    }
    public class MedicineVM
    {
        [Required]
        public string Medicine { get; set; }
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

    }
}
