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
        [Required]
        public string Diagnosis { get; set; }
        //[Required]
        public string Instruction { get; set; }
        //[Required]
        public string Test { get; set; }
        [Required]
        public string Findings { get; set; }
        [Required]
        public string Prescription { get; set; }
        
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
        public string Dose { get; set; }

    }
}
