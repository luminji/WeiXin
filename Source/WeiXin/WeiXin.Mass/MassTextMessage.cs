using System.Web;
using WeiXin.Attributes;

namespace WeiXin.Mass
{
    public class MassTextMessage : MassMessage
    {
        public MassTextMessage()
        {
            this.MsgType = MassMessageType.Text;
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
