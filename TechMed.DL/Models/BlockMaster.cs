using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class BlockMaster
    {
        public int Id { get; set; }
        public string BlockName { get; set; } = null!;
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
