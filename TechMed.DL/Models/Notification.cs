using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Notification
    {
        public long Id { get; set; }
        public int? FromUser { get; set; }
        public int ToUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; } = null!;
        public bool IsSeen { get; set; }
        public DateTime? SeenOn { get; set; }

        public virtual UserMaster? FromUserNavigation { get; set; }
        public virtual UserMaster ToUserNavigation { get; set; } = null!;
    }
}
