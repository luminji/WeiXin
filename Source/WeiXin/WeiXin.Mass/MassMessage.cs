using System.Collections.Generic;
using WeiXin.Attributes;
using WeiXin.Utilitys;

namespace WeiXin.Mass
{
    public abstract class MassMessage
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        [JsonProperty("touser")]
        public List<string> Touser { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("msgtype")]
        public MassMessageType MsgType { get; set; }

        protected string Json(string content)
        {
            string msgType = ReflectionHelper.GetCustomAttribute<JsonMsgTypeAttribute>(MsgType).MsgType;
            return "{\"touser\":[" + GetTouser() + "],\"msgtype\":\"" + msgType + "\",\"" + msgType + "\":{" + content + "}}";
        }

        private string GetTouser()
        {
            var result = string.Empty;
            if (Touser != null && Touser.Count > 0)
            {
                foreach (var item in Touser)
                {
                    result += string.Format("\"{0}\",", item);
                }
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            return result;
        }

        public abstract string GetJson();
    }
}
