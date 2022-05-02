using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseWithDoctorVM
    {
        public PatientCaseWithDoctorVM()
        {
            this.patientMaster = new PatientMasterDTO();
            this.patientCase = new PatientCaseDTO();           
            this.patientCaseQueueVM = new PatientCaseQueueVM();
            this.patientCaseMedicines = new List<PatientCaseMedicineDTO>();
        }
        public int PHCUserId { get; set; }
        public int PHCId { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public long PatientCaseID { get; set; }
        public PatientMasterDTO patientMaster { get; set; }
        public PatientCaseDTO patientCase { get; set; }
        public PatientCaseQueueVM patientCaseQueueVM { get; set; }
        public List<PatientCaseMedicineDTO> patientCaseMedicines { get; set; }

    }
}
