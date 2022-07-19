using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class OnlineDoctorVM
    {
        public int DoctorID { get; set; }
        public string DoctorFName { get; set; }
        public string DoctorMName { get; set; }
        public string DoctorLName { get; set; }
        public string Specialty { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
    }
}
