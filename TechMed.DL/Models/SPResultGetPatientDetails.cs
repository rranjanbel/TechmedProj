using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    [Keyless]
    public class SPResultGetPatientDetails
    {
        public string PHCName { get; set; }
        public string PHCAddress { get; set; }
        public string MOName { get; set; }
        public string PhoneNo { get; set; }
        public string MailID { get; set; }
        public string Cluster { get; set; }
        public string Zone { get; set; }
        public string PatientName { get; set; }
        public string PatientCreatedBy { get; set; }
        public string Docter { get; set; }
       
    }
}
