using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class CalenderMaster
    {
        public int Id { get; set; }
        public string Month { get; set; } = null!;
        public int Year { get; set; }
    }
}
