using WeiXin.Attributes;

namespace WeiXin.OAuth2
{
    public class WeiXinOAuth2AccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
