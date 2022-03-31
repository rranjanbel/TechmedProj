using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PageMaster
    {
        public int Id { get; set; }
        public string Module { get; set; } = null!;
        public string PageName { get; set; } = null!;
        public string? Detail { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
    }
}
