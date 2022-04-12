using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetEHRDTO
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string PriviousCaseLable { get; set; }
        public DateTime PriviousCaseDate { get; set; }
        public string PriviousDoctor { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public List<MedicineDTO> medicineVMs { get; set; }
        public GetEHRDTO()
        {
            medicineVMs = new List<MedicineDTO>();
        }
    }
    public class MedicineDTO
    {
        public string Medicine { get; set; }
        public string Dose { get; set; }

    }
}
