using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class SearchPatientsDTO
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public Int64 PatientID { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferredbyPHCName { get; set; }
        public string Casefile { get; set; }
        public string status { get; set; }
    }
}
