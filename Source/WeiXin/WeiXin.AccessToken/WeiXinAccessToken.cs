using System;

namespace WeiXin.AccessToken
{
    public class WeiXinAccessToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime LastGetDatetime { get; set; }
    }
}
