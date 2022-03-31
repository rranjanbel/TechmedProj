using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PageAccess
    {
        public int UserTypeId { get; set; }
        public int PageId { get; set; }

        public virtual PageMaster Page { get; set; } = null!;
        public virtual UserTypeMaster UserType { get; set; } = null!;
    }
}
