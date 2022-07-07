using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class RegisterPatientVM
    {
        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string PHCName { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public DateTime RegistrationDate { get; set; }
        //public long PatientCaseID { get; set; }
    }
}
