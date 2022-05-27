using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DoctorMeetingRoomInfo
    {
        public long Id { get; set; }
        public long? PatientCaseId { get; set; }
        public string RoomName { get; set; } = null!;
        public string MeetingSid { get; set; } = null!;
        public int? Duration { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual PatientCase? PatientCase { get; set; }
    }
}
