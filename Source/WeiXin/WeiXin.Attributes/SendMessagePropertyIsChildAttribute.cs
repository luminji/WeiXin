using System;

namespace WeiXin.Attributes.Messages
{
    /// <summary>
    /// 用于描述此属性包含子节点
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SendMessagePropertyIsChildAttribute : Attribute
    {
        public SendMessagePropertyIsChildAttribute(bool isChild)
        {
            this.IsChild = isChild;
        }

        public bool IsChild { get; set; }
    }
}
