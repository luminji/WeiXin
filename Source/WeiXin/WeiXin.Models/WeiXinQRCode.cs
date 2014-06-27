using WeiXin.Attributes.Models;
using System;

namespace WeiXin.Models
{
    [DataField(true, "[dbo].[WeiXinQRCode]")]
    public class WeiXinQRCode : IConvert
    {
        [UniqueDataField(true, "[Id]")]
        [DataField(true, "[Id]")]
        public string Id { get; set; }
        [DataField(true, "[event_key]")]
        public int EventKey { get; set; }
        [DataField(true, "[ticket]")]
        public string Ticket { get; set; }
        [DataField(true, "[expire_seconds]")]
        public int ExpireSeconds { get; set; }
        [DataField(true, "[img_url]")]
        public string ImgUrl { get; set; }
        [DataField(true, "[createTime]")]
        public DateTime CreateTime { get; set; }
    }
}
