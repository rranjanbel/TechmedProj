using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class ReportDTO
    {
        public byte[]? PrescriptionFile { get; set; }
        public string? PrescriptionFilePath { get; set; }
    }
    public class ReportResponseDTO
    {
        public string? PrescriptionFilePath { get; set; }
    }
}
