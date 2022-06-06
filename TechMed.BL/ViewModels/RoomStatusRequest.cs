using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class RoomStatusRequest
    {
        public string? AccountSid { get; set; }
        public string? RoomName { get; set; }
        public string? RoomSid { get; set; }
        public string? RoomStatus { get; set; }
        public string? RoomType { get; set; }
    }

    public class VideoCompositionStatusRequest
    {
        public string? MediaUri { get; set; }
        public int? Size { get; set; }
        public string? RoomSid { get; set; }
        public int? Duration { get; set; }
        public string? StatusCallbackEvent { get; set; }
        public string? AccountSid { get; set; }
        public string? CompositionSid { get; set; }
        public string? CompositionUri { get; set; }
    }
}
