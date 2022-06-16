using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class ServerUpTimeReport
    {
        public ServerUpTimeReport()
        {
        }
        public Int64 Id { get; set; }
        public DateTime? Date { get; set; }
        public string WorkingHours { get; set; }
        public int WorkingTime { get; set; }
        public int ServerUpTime { get; set; }
        public int ServerDownTime { get; set; }
        public string DownTimings { get; set; }
        public int Availability { get; set; }


    }

}
