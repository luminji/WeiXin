using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public class EventMessage : Message
    {
        [SendMessageProperty(false)]
        public string Event { get; set; }
        [SendMessageProperty(false)]
        public string EventKey { get; set; }
        [SendMessageProperty(false)]
        public string Ticket { get; set; }
        [SendMessageProperty(false)]
        public string Latitude { get; set; }
        [SendMessageProperty(false)]
        public string Longitude { get; set; }
        [SendMessageProperty(false)]
        public string Precision { get; set; }
    }
}