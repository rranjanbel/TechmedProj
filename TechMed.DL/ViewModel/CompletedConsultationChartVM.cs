using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class CompletedConsultationChartVM
    {
        public int NoOfConsultations { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}
