using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.Config;
using WeiXin.GlobalReturnCode;
using WeiXin.Utilitys;

namespace WeiXin.OAuth2
{
    public sealed class WeiXinOAuth2Manager
    {
        public static WeiXinOAuth2AccessToken GetToken(string code)
        {
            var result = GetOAuth2AccessToken(code);
            if (result == null)
            {
                throw new Exception("获取 OAuth2.0 access_token 失败");
            }
            return result;
        }
        private static WeiXinOAuth2AccessToken GetOAuth2AccessToken(string code)
        {
            var url = string.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code", ConfigProperty.WeiXin_OAuth2AccessTokenApi, ConfigProperty.WeiXin_AppId, ConfigProperty.WeiXin_AppSecret, code);
            try
            {
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                var returnCode = GlobalReturnCodeManager.GetReturnCode(json);
                if (returnCode.IsRequestSuccess)
                {
                    var weiXinOAuth2AccessToken = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<WeiXinOAuth2AccessToken>(json);
                    return weiXinOAuth2AccessToken;
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

        public static UserInfo GetUserInfo(string openId, string accessToken)
        {
            try
            {
                var url = string.Format("{0}?access_token={1}&openid={2}&lang=zh_CN", ConfigProperty.WeiXin_OAuth2UserInfoApi, accessToken, openId);
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                var returnCode = GlobalReturnCodeManager.GetReturnCode(json);
                if (returnCode.IsRequestSuccess)
                {
                    var userInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<UserInfo>(json);
                    return userInfo;
                }
                else
                {
                    LogHelper.LogWeiXinApiReturnCode("获取用户信息失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                }
            }
            catch (PostHttpErrorException e)
            {
                var msg = string.Format("获取用户信息失败，HTTP 状态码{0}", e.HttpStatusCode);
                LogHelper.Log(msg);
                throw new Exception(msg);
            }
            catch (Exception e)
            {
                var msg = string.Format("获取用户信息失败。\r\n错误消息：{1}", e.Message);
                LogHelper.LogError(e);
                LogHelper.Log(msg, e);
                throw new Exception(msg);
            }
            throw new Exception("获取用户信息失败");
        }
    }
}
