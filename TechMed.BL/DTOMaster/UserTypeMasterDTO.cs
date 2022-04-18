using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class UserTypeMasterDTO
    {
        public int Id { get; set; }
        public string UserType { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
