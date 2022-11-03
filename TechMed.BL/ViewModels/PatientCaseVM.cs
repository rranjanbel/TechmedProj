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
            this.caseMedicineList = new List<PatientCaseMedicineDTO>();
            this.caseDiagnosisTestList = new List<PatientCaseDiagnosisTestsVM>();
        }
        public int? PHCUserId { get; set; }
        public int PHCId { get; set; }
        public int PatientID { get; set; }
        public string PHCName { get; set; }
        public string PHCMoname { get; set; }
        public string DoctorName { get; set; }
        public string DoctorMobileNo { get; set; }
        public string DoctorSpecialization { get; set; }
        public string DoctorQalification { get; set; }
        public string DoctorMCINo { get; set; }
        public string DoctorSignature { get; set; }
        public int CaseFileStatusID { get; set; }
        public string VideoCallStatus { get; set; }
        public string? Prescription { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? AssignedOn { get; set; }
        public PatientMasterDTO patientMaster { get; set; }
        public PatientCaseDTO patientCase { get; set; }
        public List<PatientCaseVitalsVM> vitals { get; set; }
        public PatientCaseDocDTO caseDocuments { get; set; }
        public List<PatientCaseDocDTO> caseDocumentList { get; set; }
        public List<PatientCaseMedicineDTO> caseMedicineList { get; set; }
        public List<PatientCaseDiagnosisTestsVM> caseDiagnosisTestList { get; set; }

    }
}
