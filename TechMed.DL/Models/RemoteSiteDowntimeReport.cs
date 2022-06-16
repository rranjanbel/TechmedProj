using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class RemoteSiteDowntimeReport
    {
        public RemoteSiteDowntimeReport()
        {

        }

        public Int64 ID { get; set; }
        public int PHCId { get; set; }
        public DateTime? Date { get; set; }
        public int PHCDownTime { get; set; }
        public virtual Phcmaster PHC { get; set; } = null!;

    }
}
