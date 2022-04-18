using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class CompletedConsultationDTO
    {
        //PatientName
        //PatientID
        //Gender
        //Age
        //PhoneNumber
        //ReferredbyPHCName
        //Casefile 
        //status(Queued or Pending). 
        public string PatientName { get; set; }
        public Int64 PatientID { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferredbyPHCName { get; set; }
    }
}
