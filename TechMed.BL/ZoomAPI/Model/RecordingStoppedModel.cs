using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
    public class RecordingStoppedModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Object
        {
            public string uuid { get; set; }
            public long id { get; set; }
            public string host_id { get; set; }
            public string topic { get; set; }
            public int type { get; set; }
            public string start_time { get; set; }
            public string timezone { get; set; }
            public long duration { get; set; }
            public RecordingFile? recording_file { get; set; }
        }

        public class Payload
        {
            public string account_id { get; set; }
            public Object @object { get; set; }
        }

        public class RecordingFile
        {
            public string? recording_start { get; set; }
            public string? recording_end { get; set; }
        }

        public class Root
        {
            public string @event { get; set; }
            public Payload payload { get; set; }
            public long event_ts { get; set; }
        }


    }
}
