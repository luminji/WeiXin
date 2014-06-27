using System;

namespace WeiXin.Attributes.Messages
{
    /// <summary>
    /// 用于描述此属性是否是发送消息的必要属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SendMessagePropertyAttribute : Attribute
    {
        public SendMessagePropertyAttribute(bool isRequired)
        {
            this.IsRequired = isRequired;
        }

        public bool IsRequired { get; set; }
    }
}
