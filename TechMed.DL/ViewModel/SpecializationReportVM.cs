using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class SpecializationReportVM
    {
        public int Count { get; set; }
        public int SpecializationID { get; set; }
        public string Specialization { get; set; }
    }
}
