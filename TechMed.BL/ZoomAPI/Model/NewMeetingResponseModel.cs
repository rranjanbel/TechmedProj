﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model.Response
{
 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    

    public class NewMeetingResponseModel
    {
        public string uuid { get; set; }
        public long id { get; set; }
        public string host_id { get; set; }
        public string host_email { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public string status { get; set; }
        public DateTime start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public DateTime created_at { get; set; }
        public string start_url { get; set; }
        public string join_url { get; set; }
        public string password { get; set; }
        public string h323_password { get; set; }
        public string pstn_password { get; set; }
        public string encrypted_password { get; set; }
        public Settings settings { get; set; }
        public bool pre_schedule { get; set; }
    }

    public class Settings
    {
        public bool host_video { get; set; }
        public bool participant_video { get; set; }
        public bool cn_meeting { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public int jbh_time { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool watermark { get; set; }
        public bool use_pmi { get; set; }
        public int approval_type { get; set; }
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool enforce_login { get; set; }
        public string enforce_login_domains { get; set; }
        public string alternative_hosts { get; set; }
        public bool alternative_host_update_polls { get; set; }
        public bool close_registration { get; set; }
        public bool show_share_button { get; set; }
        public bool allow_multiple_devices { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool waiting_room { get; set; }
        public bool request_permission_to_unmute_participants { get; set; }
        public List<string> global_dial_in_countries { get; set; }
        public List<GlobalDialInNumber> global_dial_in_numbers { get; set; }
        public bool registrants_email_notification { get; set; }
        public bool meeting_authentication { get; set; }
        public string encryption_type { get; set; }
        public ApprovedOrDeniedCountriesOrRegions approved_or_denied_countries_or_regions { get; set; }
        public BreakoutRoom breakout_room { get; set; }
        public bool alternative_hosts_email_notification { get; set; }
        public bool device_testing { get; set; }
        public bool focus_mode { get; set; }
        public bool private_meeting { get; set; }
        public bool email_notification { get; set; }
        public bool host_save_video_order { get; set; }
    }
    public class ApprovedOrDeniedCountriesOrRegions
    {
        public bool enable { get; set; }
    }

    public class BreakoutRoom
    {
        public bool enable { get; set; }
    }

    public class GlobalDialInNumber
    {
        public string country_name { get; set; }
        public string city { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string country { get; set; }
    }

}
