using WeiXin.Attributes.SendMessage;
using WeiXin.Utilitys;

namespace WeiXin.SendMessage
{
    public abstract class CustomerServiceMessage
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        [JsonProperty("touser")]
        public string Touser { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("msgtype")]
        public CustomerServiceMessageType MsgType { get; set; }

        protected string Json(string content)
        {
            string msgType = ReflectionHelper.GetCustomAttribute<JsonMsgTypeAttribute>(MsgType).MsgType;
            return "{\"touser\":\"" + Touser + "\",\"msgtype\":\"" + msgType + "\",\"" + msgType + "\":{" + content + "}}";
        }

        public abstract string GetJson();
    }
}
