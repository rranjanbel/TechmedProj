using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class TwilioVideoDownloadStatus
    {
        public TwilioVideoDownloadStatus()
        {

        }
        public long Id { get; set; }
        public long TwilioMeetingRoomInfoId { get; set; }
        public int Attempt { get; set; }
        public string? Status { get; set; }
        public DateTime StatusAt { get; set; }
        public string? Message { get; set; }
        public string? DownloadURL { get; set; }
        public virtual TwilioMeetingRoomInfo TwilioMeetingRoomInfo { get; set; }
    }
}
