using System.Text.Json.Serialization;

namespace TechMedAPI.JwtInfra
{
    public class JwtTokenConfig
    {
        [JsonPropertyName("secret")]
#pragma warning disable CS8618 // Non-nullable property 'Secret' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Secret { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Secret' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [JsonPropertyName("issuer")]
#pragma warning disable CS8618 // Non-nullable property 'Issuer' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Issuer { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Issuer' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [JsonPropertyName("audience")]
#pragma warning disable CS8618 // Non-nullable property 'Audience' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Audience { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Audience' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [JsonPropertyName("accessTokenExpiration")]
        public int AccessTokenExpiration { get; set; }

        [JsonPropertyName("refreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; }
    }
}
