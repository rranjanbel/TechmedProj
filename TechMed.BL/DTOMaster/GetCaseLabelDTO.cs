using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetCaseLabelDTO
    {
        public long CaseID { get; set; }
        public string CaseLabel { get; set; }
        public DateTime CaseDateTime { get; set; }

        
    }
}
