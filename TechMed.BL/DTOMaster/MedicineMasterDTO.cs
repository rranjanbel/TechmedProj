using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class MedicineMasterDTO
    {
        public int Id { get; set; }
        public string MedicineName { get; set; } = null!;
        public string? Details { get; set; }
        public bool IsActive { get; set; }
    }
}
