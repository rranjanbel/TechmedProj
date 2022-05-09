using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    [Keyless]
    public class LoggedUserDetails
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("userId")]
        public int UserID { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("userRole")]
        public string UserRole { get; set; }
        public bool? IsPasswordChanged { get; set; }
    }
}
