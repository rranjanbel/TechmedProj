using System.Text.Json.Serialization;

namespace TechMed.API.Services
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
