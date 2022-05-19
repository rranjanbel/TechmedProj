using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PHCConsultationVM
    {
        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string PHCName { get; set; }
        public DateTime ConsultationDate { get; set; }
        public int NoOfConsultation { get; set; }
    }
}
