using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardEquipmentUptimeReportVM
    {
        public long SrNo { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? PHCName { get; set; }
        public int? WokingDays { get; set; }
        public int? Otoscope { get; set; }
        public int? Dermascope { get; set; }
        public int? FetalDoppler { get; set; }
        public int? Headphone { get; set; }
        public int? WebCam { get; set; }
        public int? Printer { get; set; }
        public int? Inverter { get; set; }
        public int? Computer { get; set; }
                           

    }
}
