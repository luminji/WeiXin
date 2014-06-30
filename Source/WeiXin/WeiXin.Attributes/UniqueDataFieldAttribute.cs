using System;

namespace WeiXin.Attributes
{
    /// <summary>
    /// 用于描述属性是否是数据库唯一字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueDataFieldAttribute : Attribute
    {
        public UniqueDataFieldAttribute(bool isUnique, string dataFieldName)
        {
            this.IsUnique = isUnique;
            this.DataFieldName = dataFieldName;
        }
        /// <summary>
        /// 是否是数据库唯一字段
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 对应表字段名称
        /// </summary>
        public string DataFieldName { get; set; }
    }
}
