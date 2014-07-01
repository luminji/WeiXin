using System.Collections.Generic;

namespace WeiXin.GlobalReturnCode
{
    public struct ReturnCode
    {
        /// <summary>
        /// 全局返回值
        /// </summary>
        public int ErrCode { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的 Json
        /// </summary>
        public string Json { get; set; }
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool IsRequestSuccess { get; set; }
    }
}
