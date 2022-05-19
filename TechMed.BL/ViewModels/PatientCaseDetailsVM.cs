using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseDetailsVM
    {
        public PatientCaseDetailsVM()
        {          
            this.patientCase = new PatientCaseDTO();
            this.vitals = new List<PatientCaseVitalsVM>();
            this.caseDocuments = new List<PatientCaseDocDTO>();
        }
        public int? PHCUserId { get; set; }
        public int PHCId { get; set; }
        public int PatientID { get; set; }       
        public PatientCaseDTO patientCase { get; set; }
        public List<PatientCaseVitalsVM> vitals { get; set; }
        public List<PatientCaseDocDTO> caseDocuments { get; set; }
    }
}
