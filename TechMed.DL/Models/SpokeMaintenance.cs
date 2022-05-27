using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class SpokeMaintenance
    {
        public int Id { get; set; }
        public int Phcid { get; set; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; } = null!;

        public virtual Phcmaster Phc { get; set; } = null!;
    }
}
