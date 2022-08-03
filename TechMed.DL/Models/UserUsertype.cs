using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class UserUsertype
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }

        public virtual UserMaster User { get; set; } = null!;
        public virtual UserTypeMaster UserType { get; set; } = null!;
    }
}
