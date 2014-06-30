using WeiXin.Attributes;

namespace WeiXin.Messages
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : Message
    {
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        [SendMessageProperty(true)]
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        [SendMessageProperty(false)]
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        [SendMessageProperty(false)]
        public string Recognition { get; set; }
    }
}
