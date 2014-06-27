using System;

namespace WeiXin.Attributes.Models
{
    /// <summary>
    /// 用于描述属性是否是数据库字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class DataFieldAttribute : Attribute
    {
        public DataFieldAttribute(bool isDataField, string dataFieldName = null)
        {
            this.IsDataField = isDataField;
            this.DataFieldName = dataFieldName;
        }
        /// <summary>
        /// 是否是数据库字段
        /// </summary>
        public bool IsDataField { get; set; }
        /// <summary>
        /// 对应表字段名称
        /// </summary>
        public string DataFieldName { get; set; }
    }
}
