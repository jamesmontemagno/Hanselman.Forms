using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Hanselman.Admin.Auth
{
    public class AuthInfo // structure based on sample here: https://cgillum.tech/2016/03/07/app-service-token-store/
    {
        [JsonProperty("access_token")]
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("provider_name")]
        [JsonPropertyName("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("user_id")]
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_claims")]
        [JsonPropertyName("user_claims")]
        public AuthUserClaim[] UserClaims { get; set; }

        [JsonProperty("access_token_secret")]
        [JsonPropertyName("access_token_secret")]
        public string AccessTokenSecret { get; set; }

        [JsonProperty("authentication_token")]
        [JsonPropertyName("authentication_token")]
        public string AuthenticationToken { get; set; }

        [JsonProperty("expires_on")]
        [JsonPropertyName("expires_on")]
        public string ExpiresOn { get; set; }

        [JsonProperty("id_token")]
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("refresh_token")]
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
    public class AuthUserClaim
    {

        [JsonProperty("typ")]
        [JsonPropertyName("typ")]
        public string Type { get; set; }

        [JsonProperty("val")]
        [JsonPropertyName("val")]
        public string Value { get; set; }
    }
}