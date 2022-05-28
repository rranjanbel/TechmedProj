using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseVM
    {
        public PatientCaseVM()
        {
            this.patientMaster = new PatientMasterDTO();
            this.patientCase = new PatientCaseDTO();
            this.vitals = new List<PatientCaseVitalsVM>();
            this.caseDocuments = new PatientCaseDocDTO();
            this.caseDocumentList = new List<PatientCaseDocDTO>();
        }
        public int? PHCUserId { get; set; }
        public int PHCId { get; set; }
        public int PatientID { get; set; }
        public string PHCName { get; set; }
        public string PHCMoname { get; set; }
        public PatientMasterDTO patientMaster { get; set; }
        public PatientCaseDTO patientCase { get; set; }
        public List<PatientCaseVitalsVM> vitals { get; set; }
        public PatientCaseDocDTO caseDocuments { get; set; }
        public List<PatientCaseDocDTO> caseDocumentList { get; set; }
    }
}
