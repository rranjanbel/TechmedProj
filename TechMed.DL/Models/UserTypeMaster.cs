using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class UserTypeMaster
    {
        public int Id { get; set; }
        public string UserType { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
