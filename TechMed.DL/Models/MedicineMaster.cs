using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class MedicineMaster
    {
        public int Id { get; set; }
        public string MedicineName { get; set; } = null!;
        public string? Details { get; set; }
        public bool IsActive { get; set; }
    }
}
