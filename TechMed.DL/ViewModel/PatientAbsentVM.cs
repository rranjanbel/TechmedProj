using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class PatientAbsentVM
    {
        [Required]
        public long CaseID { get; set; }
        [Required]
        public long DoctorID { get; set; }
        //[Required]
        public string Comment { get; set; }
        
    }
}
