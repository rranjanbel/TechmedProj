using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class HolidayMaster
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int CalenderId { get; set; }

        public virtual CalenderMaster Calender { get; set; } = null!;
    }
}
