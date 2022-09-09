using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class LoginHistory
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime LogedInTime { get; set; }
        public DateTime? LogedoutTime { get; set; }
        public string? UserToken { get; set; }
        public DateTime? LastUpdateOn { get; set; }
        public virtual UserMaster User { get; set; } = null!;
        public virtual UserTypeMaster UserType { get; set; } = null!;
    }
}
