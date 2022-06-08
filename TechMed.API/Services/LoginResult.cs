using System.Text.Json.Serialization;

namespace TechMed.API.Services
{
    public class LoginResult
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("originalUserName")]
        public string OriginalUserName { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("roleName")]
        public string roleName { get; set; }

        [JsonPropertyName("isOnOtherDevice")]
        public bool isOnOtherDevice { get; set; }

        [JsonPropertyName("isPasswordChanged")]
        public bool? isPasswordChanged { get; set; }


    }
}
