using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class TwilioMeetingRoomInfo
    {
        public TwilioMeetingRoomInfo()
        {
            TwilioVideoDownloadStatuses = new HashSet<TwilioVideoDownloadStatus>();
        }
        public long Id { get; set; }
        public long? PatientCaseId { get; set; }
        public string RoomName { get; set; } = null!;
        public string MeetingSid { get; set; } = null!;
        public decimal? Duration { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CompositeVideoSid { get; set; }
        public string? TwilioRoomStatus { get; set; }
        public string? RoomStatusCallback { get; set; }
        public long? CompositeVideoSize { get; set; }
        public string? CompositionUri { get; set; }
        public string? MediaUri { get; set; }
        public string? CompositionStatus { get; set; }

        public virtual PatientCase? PatientCase { get; set; }
        public int? AssignedDoctorId { get; set; } = null!;
        public int? AssignedBy { get; set; } = null!;
        public virtual Phcmaster AssignedByNavigation { get; set; } = null!;
        public virtual DoctorMaster AssignedDoctor { get; set; } = null!;
        public virtual ICollection<TwilioVideoDownloadStatus> TwilioVideoDownloadStatuses { get; set; }
    }
}
