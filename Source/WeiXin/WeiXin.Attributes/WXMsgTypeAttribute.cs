using System;

namespace WeiXin.Attributes
{
    /// <summary>
    /// 用于描述消息类型对应的微信接口 MsgType 文本值
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class WXMsgTypeAttribute : Attribute
    {
        public WXMsgTypeAttribute(string msgType)
        {
            this.MsgType = msgType;
        }

        public string MsgType { get; set; }
    }
}
