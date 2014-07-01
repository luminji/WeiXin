using WeiXin.Attributes;

namespace WeiXin.QRCode
{
    public class QRCodeInfo
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
        [JsonProperty("expire_seconds")]
        public int ExpireSeconds { get; set; }
    }
}
