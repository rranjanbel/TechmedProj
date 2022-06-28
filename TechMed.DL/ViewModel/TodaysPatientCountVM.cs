using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class TodaysPatientCountVM
    {
        // ID Count   PHCID PHCName Type Description       
        //public long ID { get; set; }
        public int Count { get; set; }
        public int PHCID { get; set; }
        public string PHCName { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}
