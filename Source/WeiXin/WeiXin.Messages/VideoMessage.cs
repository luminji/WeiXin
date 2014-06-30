using WeiXin.Attributes;

namespace WeiXin.Messages
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : Message
    {
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        [SendMessageProperty(true)]
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        [SendMessageProperty(false)]
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        [SendMessageProperty(true)]
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        [SendMessageProperty(true)]
        public string Description { get; set; }
    }
}
