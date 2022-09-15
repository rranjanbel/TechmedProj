using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
 
    public class ZoomWebHookResponseModel
    {
        public string @event { get; set; }
        public dynamic payload { get; set; }
        public long event_ts { get; set; }
    }
}
