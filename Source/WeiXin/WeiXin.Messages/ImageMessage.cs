using WeiXin.Attributes;

namespace WeiXin.Messages
{
    public class ImageMessage : Message
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        [SendMessageProperty(false)]
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        [SendMessageProperty(true)]
        public string MediaId { get; set; }

    }
}
