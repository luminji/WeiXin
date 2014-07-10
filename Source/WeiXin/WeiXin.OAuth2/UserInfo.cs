using WeiXin.Attributes;

namespace WeiXin.OAuth2
{
    public class UserInfo
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public int Sex { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("headimgurl")]
        public string HeadImgUrl { get; set; }
    }
}
