using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class VideoCallTransaction
    {
        public long Id { get; set; }
        public int FromUserId { get; set; }
        public int PatientId { get; set; }
        public int ToUserId { get; set; }
        public string RoomId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? RecordingLink { get; set; }

        public virtual UserMaster FromUser { get; set; } = null!;
        public virtual PatientMaster Patient { get; set; } = null!;
        public virtual UserMaster ToUser { get; set; } = null!;
    }
}
