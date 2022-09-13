using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class ZoomWebHookEvent
    {
        public long ID { get; set; }
        public string RequestValue { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

    }
}
