using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class CaseDocumentBase64VM
    {
        public string name { get; set; }
        public string file { get; set; }
        public int id { get; set; }
        public Int64 patientCaseId { get; set; }
        public int DocumentTypeId { get; set; }
    }
}
