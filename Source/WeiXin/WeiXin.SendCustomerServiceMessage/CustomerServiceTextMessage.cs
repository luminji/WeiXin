using System.Web;
using WeiXin.Attributes;

namespace WeiXin.SendCustomerServiceMessage
{
    public class CustomerServiceTextMessage : CustomerServiceMessage
    {
        public CustomerServiceTextMessage()
        {
            this.MsgType = CustomerServiceMessageType.Text;
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        public override string GetJson()
        {
            string content = string.Format("\"content\":\"{0}\"", HttpUtility.UrlDecode(this.Content));
            return base.Json(content);
        }
    }
}
