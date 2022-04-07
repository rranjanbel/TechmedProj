using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseVM
    {
        public PatientCaseVM()
        {
            this.patientCase = new PatientCaseDTO();
            this.vitalMaster = new VitalMasterDTO();
            this.caseDocument = new PatientCaseDocumentDTO();
        }
        public int PHCUserId { get; set; }
        public int PHCId { get; set; }
        public PatientCaseDTO patientCase { get; set; }
        public VitalMasterDTO vitalMaster { get; set; }
        public PatientCaseDocumentDTO caseDocument { get; set; }
    }
}
