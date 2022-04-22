using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class UserTypeMaster
    {
        public UserTypeMaster()
        {
            LoginHistories = new HashSet<LoginHistory>();
        }

        public int Id { get; set; }
        public string UserType { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
    }
}
