using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class TreatmentVM
    {
        public long PatientCaseID { get; set; }
        public string Diagnosis { get; set; }
        public string Instruction { get; set; }
        public string Test { get; set; }
        public string Findings { get; set; }
        public List<MedicineVM> medicineVMs { get; set; }
        public TreatmentVM()
        {
            medicineVMs = new List<MedicineVM>();
        }
    }
    public class MedicineVM
    {
        public string Medicine { get; set; }
        public string Dose { get; set; }

    }
}
