using WeiXin.Attributes.Models;
using System;

namespace WeiXin.Models
{
    [DataField(true, "[dbo].[WeiXinAccessTokenTest]")]
    public class WeiXinAccessToken : IConvert
    {
        [UniqueDataField(true, "[AccessToken]")]
        [DataField(true, "[AccessToken]")]
        public string AccessToken { get; set; }
        [DataField(true, "[ExpiresIn]")]
        public int ExpiresIn { get; set; }
        [DataField(true, "[LastGetDatetime]")]
        public DateTime LastGetDatetime { get; set; }
    }
}
