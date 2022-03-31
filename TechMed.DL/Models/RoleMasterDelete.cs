using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class RoleMasterDelete
    {
        public long Id { get; set; }
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
