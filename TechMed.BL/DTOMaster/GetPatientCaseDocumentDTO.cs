using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetPatientCaseDocumentDTO
    {
        public long Id { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
        public string? Description { get; set; }
    }
}
