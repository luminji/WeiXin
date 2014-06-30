using WeiXin.Attributes;

namespace WeiXin.UserManager
{
    public class UserInfo
    {
        [JsonProperty("subscribe")]
        public int Subscribe { get; set; }
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("sex")]
        public int Sex { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("headimgurl")]
        public string HeadImgUrl { get; set; }
        [JsonProperty("subscribe_time")]
        public int SubscribeTime { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
    }
}
