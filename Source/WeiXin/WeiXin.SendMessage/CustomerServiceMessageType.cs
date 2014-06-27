using WeiXin.Attributes.SendMessage;

namespace WeiXin.SendMessage
{
    /// <summary>
    /// 客服消息类型
    /// </summary>
    public enum CustomerServiceMessageType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [JsonMsgType("text")]
        Text = 0,
        /// <summary>
        /// 图片消息
        /// </summary>
        [JsonMsgType("image")]
        Image = 1,
        /// <summary>
        /// 语音消息
        /// </summary>
        [JsonMsgType("voice")]
        Voice = 2,
        /// <summary>
        /// 视频消息
        /// </summary>
        [JsonMsgType("video")]
        Video = 3,
        /// <summary>
        /// 音乐消息
        /// </summary>
        [JsonMsgType("music")]
        Music = 4,
        /// <summary>
        /// 图文消息
        /// </summary>
        [JsonMsgType("news")]
        News = 5
    }
}
