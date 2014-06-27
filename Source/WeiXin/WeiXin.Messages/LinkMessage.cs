using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class LinkMessage : Message
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        [SendMessageProperty(false)]
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        [SendMessageProperty(false)]
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        [SendMessageProperty(false)]
        public string Url { get; set; }
    }
}
