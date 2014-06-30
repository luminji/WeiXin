using WeiXin.Attributes;

namespace WeiXin.Mass
{
    /// <summary>
    /// 群发消息类型
    /// </summary>
    public enum MassMessageType
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        [JsonMsgType("mpnews")]
        Mpnews = 0,
        /// <summary>
        /// 文本消息
        /// </summary>
        [JsonMsgType("text")]
        Text = 1,
        /// <summary>
        /// 语音消息
        /// </summary>
        [JsonMsgType("voice")]
        Voice = 2,
        /// <summary>
        /// 图片消息
        /// </summary>
        [JsonMsgType("image")]
        Image = 3,
        /// <summary>
        /// 视频消息
        /// </summary>
        [JsonMsgType("video")]
        Video = 4
    }
}
