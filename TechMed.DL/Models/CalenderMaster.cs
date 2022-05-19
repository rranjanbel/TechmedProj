using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class CalenderMaster
    {
        public CalenderMaster()
        {
            HolidayMasters = new HashSet<HolidayMaster>();
        }

        public int Id { get; set; }
        public string Month { get; set; } = null!;
        public int Year { get; set; }

        public virtual ICollection<HolidayMaster> HolidayMasters { get; set; }
    }
}
