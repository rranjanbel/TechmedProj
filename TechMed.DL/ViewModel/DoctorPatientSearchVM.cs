using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class DoctorPatientSearchVM
    {
        public string PatientName { get; set; }
        public long PatientID { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public int DocterID { get; set; }
        public string Doctor { get; set; }
        public int ID { get; set; } 
    }
}
