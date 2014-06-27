using System;

namespace WeiXin.Attributes.SendMessage
{
    /// <summary>
    /// 用于描述客服消息类型对应的 json 数据
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class JsonMsgTypeAttribute : Attribute
    {
        public JsonMsgTypeAttribute(string msgType)
        {
            this.MsgType = msgType;
        }
        /// <summary>
        /// 对应消息类型
        /// </summary>
        public string MsgType { get; set; }
    }
}
