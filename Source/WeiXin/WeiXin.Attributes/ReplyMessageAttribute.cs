using System;

namespace WeiXin.Attributes.Messages
{
    /// <summary>
    /// 用于描述消息类型是否需要回复
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ReplyMessageAttribute : Attribute
    {
        public ReplyMessageAttribute(bool isReply)
        {
            this.IsReply = isReply;
        }

        public bool IsReply { get; set; }
    }
}
