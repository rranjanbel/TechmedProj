using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{
   
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
      

        public class NewMeetingRequestModel
        {
            public NewMeetingRequestModel()
            {
                settings = new Settings();
            }
            public string agenda { get; set; }="Telemed Meeting";
            public bool default_password { get; set; }=false;
            public int duration { get; set; } = 30;
            public string password { get; set; } = "Telemed@123456";
            public string schedule_for { get; set; }
            public Settings settings { get; set; }
            public DateTime start_time { get; set; }
            public string template_id { get; set; } = "Dv4YdINdTk+Z5RToadh5ug==";
            public string timezone { get; set; } = "Asia/Calcutta";
            public string topic { get; set; } = "Telemed Meeting";
            public int type { get; set; } = 2;

        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
       

        public class Settings
        {
            public bool allow_multiple_devices { get; set; } = true;
            public string audio { get; set; } = "telephony";
            public string authentication_domains { get; set; } = "example.com";
            public string authentication_option { get; set; } = "signIn_D8cJuqWVQ623CI4Q8yQK0Q";
            public string auto_recording { get; set; } = "cloud";
            public bool email_notification { get; set; } = false;
            public string encryption_type { get; set; } = "enhanced_encryption";
            public bool focus_mode { get; set; } = true;
            public bool host_video { get; set; } = true;
            public int jbh_time { get; set; } = 0;
            public bool join_before_host { get; set; } = true;
            public bool meeting_authentication { get; set; }= false;
            public bool participant_video { get; set; } = true;
            public bool private_meeting { get; set; } = false;
            public bool registrants_confirmation_email { get; set; } = true;
            public bool registrants_email_notification { get; set; } = true;
            public int registration_type { get; set; }=1;
            public bool show_share_button { get; set; } = false;
            public bool host_save_video_order { get; set; } = true;
            public bool alternative_host_update_polls { get; set; } = true;
        }

    
}
