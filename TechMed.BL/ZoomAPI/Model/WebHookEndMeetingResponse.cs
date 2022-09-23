using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
    internal class WebHookEndMeetingResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Object
        {
            public long duration { get; set; }
            public DateTime start_time { get; set; }
            public string timezone { get; set; }
            public DateTime end_time { get; set; }
            public string topic { get; set; }
            public string id { get; set; }
            public int type { get; set; }
            public string uuid { get; set; }
            public string host_id { get; set; }
        }

        public class Payload
        {
            public string account_id { get; set; }
            public Object @object { get; set; }
        }

        public class WebHookEndMeetingModel
        {
            public string @event { get; set; }
            public Payload payload { get; set; }
            public long event_ts { get; set; }
        }

    }
}
