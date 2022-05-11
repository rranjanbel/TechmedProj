using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class ServerHealth
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int? Workinghours { get; set; }
        public string? Workingtime { get; set; }
        public TimeSpan? ServerUpTime { get; set; }
        public TimeSpan? ServerDownTime { get; set; }
        public string? DownTiming { get; set; }
        public string? Reason { get; set; }
        public decimal? Availability { get; set; }
    }
}
