using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class VideoCallTransactionDTO
    {
        public long Id { get; set; }
        public int FromUserId { get; set; }
        public int PatientId { get; set; }
        public int ToUserId { get; set; }
        public string? RoomId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? RecordingLink { get; set; }
    }
}
