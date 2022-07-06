using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseCreateVM
    {        
        public long CaseFileID { get; set; }
        public int PatientID { get; set; }
        public string CaseFileNumber { get; set; }
        public string CaseTitle { get; set; }
        public int SpecializationID { get; set; }
        public int CreatedBy { get; set; }
        public string OPDNumber { get; set; }
        public int? CaseStatusID { get; set; }
    }
}
