using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class CdssguidelineMaster
    {
        public int Id { get; set; }
        public string Cdssguideline { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual UserMaster? UpdatedByNavigation { get; set; }
    }
}
