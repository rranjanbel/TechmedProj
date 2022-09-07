using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<NewUserRequestModel>(myJsonResponse);
    public class NewUserRequestModel
    {
        public NewUserRequestModel()
        {
            user_info = new UserInfo(); 
        }
        public string action { get; set; }
        public UserInfo user_info { get; set; }
    }

    public class UserInfo
    {
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? password { get; set; }
        public int type { get; set; }
    }

}
