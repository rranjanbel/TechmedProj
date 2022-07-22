using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class ServerHealth
    {
        public long Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan TimeDuration { get; set; }
        public long TimeDurationSS { get; set; }
        public string? CurrentStatus { get; set; }
        public string? Details { get; set; }

    }
}
