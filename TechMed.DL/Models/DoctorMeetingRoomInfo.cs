using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DoctorMeetingRoomInfo
    {
        public long Id { get; set; }
        public int DoctorId { get; set; }
        public long? CaseId { get; set; }
        public int? PatientId { get; set; }
        public int? PhcId { get; set; }
        public string RoomName { get; set; } = null!;
        public string MeetingSid { get; set; } = null!;
        public int? Duration { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual PatientCase? Case { get; set; }
        public virtual DoctorMaster Doctor { get; set; } = null!;
        public virtual PatientMaster? Patient { get; set; }
        public virtual Phcmaster? Phc { get; set; }
    }
}
