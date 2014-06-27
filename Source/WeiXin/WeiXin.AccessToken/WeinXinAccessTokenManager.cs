using System;
using System.Collections.Generic;
using WeiXin.Config;
using WeiXin.DAL;
using WeiXin.Models;
using WeiXin.Utilitys;

namespace WeiXin.AccessToken
{
    public sealed class WeinXinAccessTokenManager
    {
        private static WeiXinAccessToken _AccessToken;
        public static string GetToken()
        {
            var isGetWeb = default(bool);
            if (_AccessToken == null)
            {
                var list = new WeiXinAccessTokenDal().Read();
                if (list != null && list.Count > 0)
                {
                    _AccessToken = list[0];
                }
            }
            if (_AccessToken != null)
            {
                var seconds = (int)DateTime.Now.Subtract(_AccessToken.LastGetDatetime).TotalSeconds;
                isGetWeb = _AccessToken.ExpiresIn - seconds < 600;
            }
            else
            {
                isGetWeb = true;
            }
            if (isGetWeb)
            {
                var webResult = GetWebToken();
                if (webResult != null)
                {
                    _AccessToken = new WeiXinAccessToken();
                    _AccessToken.AccessToken = (string)webResult["access_token"];
                    _AccessToken.ExpiresIn = int.Parse(webResult["expires_in"].ToString());
                    _AccessToken.LastGetDatetime = DateTime.Now;
                    new WeiXinAccessTokenDal().Save(_AccessToken);
                }
            }
            return _AccessToken.AccessToken;
        }
        private static Dictionary<string, object> GetWebToken()
        {
            var url = string.Format("{0}?grant_type=client_credential&appid={1}&secret={2}", ConfigProperty.WeiXin_AccessTokenApi, ConfigProperty.WeiXin_AppId, ConfigProperty.WeiXin_AppSecret);
            try
            {
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                var jsonObj = JsonSerializerHelper.Deserialize(json);
                return jsonObj;
            }
            catch (PostHttpErrorException e)
            {
                LogHelper.Log(string.Format("获取 access_token 失败，HTTP 状态码{0}", e.HttpStatusCode));
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
                LogHelper.Log("获取 access_token 失败");
            }
            return null;
        }
    }
}
