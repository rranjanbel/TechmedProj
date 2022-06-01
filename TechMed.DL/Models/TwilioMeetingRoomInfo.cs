using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    [Table("TwilioMeetingRoomInfo")]
    public partial class TwilioMeetingRoomInfo
    {
        public TwilioMeetingRoomInfo()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long PatientCaseID { get; set; }
        public string? RoomName { get; set; }
        public string? MeetingSID { get; set; }
        public Nullable<int> Duration { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsClosed { get; set; }
        public Nullable<DateTime> CloseDate { get; set; }

        public string? CompositeVideoSID { get; set; }
        public string? TwilioRoomStatus { get; set; }
        public string? RoomStatusCallback { get; set; }
        public long? CompositeVideoSize { get; set; }

        //public virtual DoctorMaster DoctorInfo { get; set; } = null!;
        public virtual PatientCase PatientCaseInfo { get; set; } = null!;
        //public virtual PatientMaster PatientMasterInfo { get; set; } = null!;
        //public virtual Phcmaster PhcMasterInfo { get; set; } = null!;
    }
}
