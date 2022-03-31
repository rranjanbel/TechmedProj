using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class VitalMaster
    {
        public int Id { get; set; }
        public string Vital { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
