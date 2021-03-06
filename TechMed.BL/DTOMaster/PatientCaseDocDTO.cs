using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientCaseDocDTO
    {
        public long Id { get; set; }
        public long PatientCaseId { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
        public string? Description { get; set; }
        public int DocumentTypeID { get; set; }
    }
}
