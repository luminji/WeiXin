using WeiXin.Attributes;

namespace WeiXin.Messages
{
    public abstract class Message
    {
        /// <summary>
        /// xml消息
        /// </summary>
        [SendMessageProperty(false)]
        public string Xml { get; set; }
        /// <summary>
        /// 接收方微信号/OpenID
        /// </summary>
        [SendMessageProperty(true)]
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方微信号/OpenID
        /// </summary>
        [SendMessageProperty(true)]
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间（整形）
        /// </summary>
        [SendMessageProperty(true)]
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [SendMessageProperty(true)]
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id，64位整形（用于排除重复消息）
        /// </summary>
        [SendMessageProperty(false)]
        public string MsgId { get; set; }
    }
}
