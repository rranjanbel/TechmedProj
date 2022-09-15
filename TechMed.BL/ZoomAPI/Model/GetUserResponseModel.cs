﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GetUserResponseModel
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int type { get; set; }
        public string role_name { get; set; }
        public long pmi { get; set; }
        public bool use_pmi { get; set; }
        public string personal_meeting_url { get; set; }
        public string timezone { get; set; }
        public int verified { get; set; }
        public string dept { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_login_time { get; set; }
        public string pic_url { get; set; }
        public string cms_user_id { get; set; }
        public string jid { get; set; }
        public List<object> group_ids { get; set; }
        public List<object> im_group_ids { get; set; }
        public string account_id { get; set; }
        public string language { get; set; }
        public string phone_country { get; set; }
        public string phone_number { get; set; }
        public string status { get; set; }
        public string job_title { get; set; }
        public string location { get; set; }
        public List<int> login_types { get; set; }
        public string role_id { get; set; }
        public long account_number { get; set; }
        public string cluster { get; set; }
    }

}
