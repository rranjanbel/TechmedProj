using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class CDSSGuideline
    {
        public CDSSGuideline()
        {

        }
        public Int64 ID { get; set; }
        public string? Age { get; set; }
        public string? Diseases { get; set; }
        public string? Treatment { get; set; }
    }
}
