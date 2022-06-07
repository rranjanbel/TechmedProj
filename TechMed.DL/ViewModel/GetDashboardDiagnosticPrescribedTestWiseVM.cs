using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardDiagnosticPrescribedTestWiseVM
    {
        public Int64 SrNo { get; set; }
        public string? Diagnostic { get; set; }
        public int NoOfTimePrescribed { get; set; }    
    }
}
