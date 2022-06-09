using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardGraphVM
    {
        public int PatientCount { get; set; }
        public string? DistrictName { get; set; } 

    }
}
