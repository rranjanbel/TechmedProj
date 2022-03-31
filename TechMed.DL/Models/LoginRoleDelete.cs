using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class LoginRoleDelete
    {
        public long Id { get; set; }
        public long LoginId { get; set; }
        public long RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
