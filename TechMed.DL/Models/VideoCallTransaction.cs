using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class VideoCallTransaction
    {
        public long Id { get; set; }
        public int FromUser { get; set; }
        public int? ToUser { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? VideoLink { get; set; }

        public virtual UserMaster FromUserNavigation { get; set; } = null!;
        public virtual UserMaster? ToUserNavigation { get; set; }
    }
}
