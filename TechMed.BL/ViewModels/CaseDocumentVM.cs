using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class CaseDocumentVM
    {
        public string name { get; set; }
        public IFormFile file { get; set; }
        public int id { get; set; }
        public long patientCaseId { get; set; }
        public int DocumentTypeId { get; set; }
    }
}
