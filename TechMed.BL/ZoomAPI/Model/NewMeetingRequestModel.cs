using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
    public class NewMeetingRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Feature
        {
            public bool zoom_phone { get; set; }
            public int zoom_one_type { get; set; }
        }

        public class NewMeetingRequestModel
        {
            public NewMeetingRequestModel()
            {
                user_info=new UserInfo();
            }
            public string action { get; set; }
            public UserInfo user_info { get; set; }
        }

        public class UserInfo
        {
            public UserInfo()
            {
                feature=new Feature();
            }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string password { get; set; }
            public int type { get; set; }
            public Feature feature { get; set; }
            public string plan_united_type { get; set; }
        }
    }
}
