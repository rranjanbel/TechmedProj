using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class SnomedCTCode
    {
        public SnomedCTCode()
        {
           
        }

        public long Id { get; set; }
        public string CodeName { get; set; } = null!;

    }
}
