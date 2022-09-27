using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.ViewModels
{
    public class PatientAddStatusVM
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public PatientMasterDTO PatientMasterDTO { get; set; }

}
}
