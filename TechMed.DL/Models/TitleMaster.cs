using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class TitleMaster
    {
        public TitleMaster()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
