using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class SymptomsMaster
    {
        public int Id { get; set; }
        public string Symptom { get; set; } = null!;
    }
}
