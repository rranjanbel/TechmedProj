using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardDiseaseAgeWiseVM
    {
        public Int64 SrNo { get; set; }
        public string? Age { get; set; }
        public string? Disease { get; set; }
        public int Occurrence { get; set; }


    }
}
