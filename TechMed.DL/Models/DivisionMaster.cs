using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DivisionMaster
    {
        public int Id { get; set; }
        public string DivisionName { get; set; } = null!;
        public int ClusterId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
