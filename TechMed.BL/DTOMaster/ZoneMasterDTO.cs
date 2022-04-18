using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class ZoneMasterDTO
    {
        public int Id { get; set; }
        public int ClusterId { get; set; }
        public string Zone { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
