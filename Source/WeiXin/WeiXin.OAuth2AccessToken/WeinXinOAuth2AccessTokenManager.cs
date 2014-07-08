using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.Config;
using WeiXin.GlobalReturnCode;
using WeiXin.Utilitys;

namespace WeiXin.OAuth2AccessToken
{
    public sealed class WeinXinOAuth2AccessTokenManager
    {
        public static string GetOpenId(string code)
        {
            LogHelper.Log("code:" + code);
            var result = GetOAuth2AccessToken(code);
            if (result == null)
            {
                throw new Exception("获取 OAuth2.0 access_token 失败");
            }
            return (string)result["openid"];
        }
        private static Dictionary<string, object> GetOAuth2AccessToken(string code)
        {
            var url = string.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code", ConfigProperty.WeiXin_OAuth2AccessTokenApi, ConfigProperty.WeiXin_AppId, ConfigProperty.WeiXin_AppSecret, code);
            try
            {
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                var returnCode = GlobalReturnCodeManager.GetReturnCode(json);
                if (returnCode.IsRequestSuccess)
                {
                    var jsonObj = JsonSerializerHelper.Deserialize(json);
                    return jsonObj;
                }
                else
                {
                    LogHelper.LogWeiXinApiReturnCode("获取 OAuth2.0 access_token 失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                    return null;
                }
            }
            catch (PostHttpErrorException e)
            {
                LogHelper.Log(string.Format("获取 OAuth2.0 access_token 失败，HTTP 状态码{0}", e.HttpStatusCode));
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
                LogHelper.Log("获取 OAuth2.0 access_token 失败");
            }
            return null;
        }
    }
}
