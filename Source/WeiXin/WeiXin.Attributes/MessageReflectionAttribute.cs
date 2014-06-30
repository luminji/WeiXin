using System;

namespace WeiXin.Attributes
{
    /// <summary>
    /// 用于描述消息类型对应的消息实体
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MessageReflectionAttribute : Attribute
    {
        public MessageReflectionAttribute(string assemblyString, string className)
        {
            this.AssemblyString = assemblyString;
            this.ClassName = className;
        }
        public string AssemblyString { get; set; }
        public string ClassName { get; set; }
    }
}
