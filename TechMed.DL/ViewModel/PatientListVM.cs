using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class PatientListVM
    {
        public string DoctorName { get; set; }
        public List<PatientsVM> Patients { get; set; }
    }
}
