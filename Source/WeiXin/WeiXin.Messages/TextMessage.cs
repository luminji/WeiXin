using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : Message
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [SendMessageProperty(true)]
        public string Content { get; set; }
    }
}
