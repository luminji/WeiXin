using System;
using System.Collections;
using System.Collections.Generic;
using WeiXin.AccessToken;
using WeiXin.Config;
using WeiXin.Utilitys;

namespace WeiXin.UserManager
{
    public sealed class WeiXinUserManager
    {
        /// <summary>
        /// 获取已关注用户列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUserList()
        {
            var result = new List<string>();
            var accessToken = WeinXinAccessTokenManager.GetToken();
            var next_openid = string.Empty;
            var api = ConfigProperty.WeiXin_GetUserListApi;
            var total = 0;
            var readCount = 0;
            do
            {
                var url = string.Empty;
                if (string.IsNullOrEmpty(next_openid))
                {
                    url = string.Format("{0}?access_token={1}", api, accessToken);
                }
                else
                {
                    url = string.Format("{0}?access_token={1}&next_openid={2}", api, accessToken, next_openid);
                }
                try
                {
                    var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                    var jsonObj = JsonSerializerHelper.Deserialize(json);
                    total = Convert.ToInt32(jsonObj["total"]);
                    var count = Convert.ToInt32(jsonObj["count"]);
                    next_openid = Convert.ToString(jsonObj["next_openid"]);
                    readCount += count;
                    if (count > 0)
                    {
                        var openids = (jsonObj["data"] as Dictionary<string, object>)["openid"];
                        if (openids is ArrayList)
                        {
                            foreach (var item in openids as ArrayList)
                            {
                                result.Add(item.ToString());
                            }
                        }
                        else
                        {
                            LogHelper.Log("获取已关注列表转换 openid 类型失败，实际类型：" + openids.ToString());
                        }
                    }
                }
                catch (PostHttpErrorException e)
                {
                    LogHelper.Log(string.Format("获取已关注列表失败，HTTP 状态码{0}", e.HttpStatusCode));
                }
                catch (Exception e)
                {
                    LogHelper.LogError(e);
                    LogHelper.Log("获取已关注列表失败");
                }
            } while (total > readCount);
            return result;
        }
        /// <summary>
        /// 获取已关注用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string openId)
        {
            if (!string.IsNullOrEmpty(openId))
            {
                try
                {
                    var accessToken = WeinXinAccessTokenManager.GetToken();
                    var url = string.Format("{0}?access_token={1}&openid={2}&lang=zh_CN", ConfigProperty.WeiXin_GetUserInfoApi, accessToken, openId);
                    var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                    var userInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<UserInfo>(json);
                    return userInfo;
                }
                catch (PostHttpErrorException e)
                {
                    LogHelper.Log(string.Format("获取已关注列表失败，HTTP 状态码{0}", e.HttpStatusCode));
                }
                catch (Exception e)
                {
                    LogHelper.LogError(e);
                    LogHelper.Log("获取已关注列表失败");
                }
            }
            return null;
        }
    }
}
