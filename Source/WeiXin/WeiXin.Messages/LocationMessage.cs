using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationMessage : Message
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        [SendMessageProperty(false)]
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        [SendMessageProperty(false)]
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        [SendMessageProperty(false)]
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        [SendMessageProperty(false)]
        public string Label { get; set; }
    }
}
