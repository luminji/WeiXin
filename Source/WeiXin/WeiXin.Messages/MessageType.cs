using WeiXin.Attributes;
using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 微信消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [ReplyMessage(false)]
        Undefined = 0,
        /// <summary>
        /// 文本消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "TextMessage")]
        [WXMsgType("text")]
        Text = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "ImageMessage")]
        [WXMsgType("image")]
        Image = 2,
        /// <summary>
        /// 语音消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "VoiceMessage")]
        [WXMsgType("voice")]
        Voice = 3,
        /// <summary>
        /// 视频消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "VideoMessage")]
        [WXMsgType("video")]
        Video = 4,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "LocationMessage")]
        [WXMsgType("location")]
        Location = 5,
        /// <summary>
        /// 链接消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "LinkMessage")]
        [WXMsgType("link")]
        Link = 6,
        /// <summary>
        /// 事件消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "EventMessage")]
        [WXMsgType("event")]
        Event = 7,
        /// <summary>
        /// 文本消息
        /// </summary>
        [ReplyMessage(true)]
        [MessageReflection("WeiXin.Messages", "NewsMessage")]
        [WXMsgType("news")]
        News = 8
    }
}
