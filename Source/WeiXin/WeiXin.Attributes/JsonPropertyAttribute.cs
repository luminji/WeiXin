using System;

namespace WeiXin.Attributes
{
    /// <summary>
    /// 用于描述属性对应的客服消息 json 属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonPropertyAttribute : Attribute
    {
        public JsonPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }
        /// <summary>
        /// 对应属性名称
        /// </summary>
        public string PropertyName { get; set; }
    }
}
