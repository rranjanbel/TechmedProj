using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class OfficialWorkingHour
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
