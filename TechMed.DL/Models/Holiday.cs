using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Description { get; set; } = null!;
    }
}
