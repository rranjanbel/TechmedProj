using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class SubSpecializationDTO
    {
        public int Id { get; set; }
        public int SpecializationId { get; set; }
        public string SubSpecialization { get; set; } = null!;
    }
}
