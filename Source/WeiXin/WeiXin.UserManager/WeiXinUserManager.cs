using System;
using System.Collections;
using System.Collections.Generic;
using WeiXin.AccessToken;
using WeiXin.Config;
using WeiXin.GlobalReturnCode;
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
            var next_openid = string.Empty;
            var api = ConfigProperty.WeiXin_GetUserListApi;
            var total = 0;
            var readCount = 0;
            string accessToken = null;
            try
            {
                accessToken = WeiXinAccessTokenManager.GetToken();
            }
            catch (Exception e)
            {
                LogHelper.Log("获取已关注列表失败", e);
                LogHelper.LogError(e);
                throw new Exception(string.Format("获取已关注列表失败。\r\n错误消息：\r\n{1}", e.Message));
            }
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
                    var returnCode = GlobalReturnCodeManager.GetReturnCode(json);
                    if (returnCode.IsRequestSuccess)
                    {
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
                    else
                    {
                        LogHelper.LogWeiXinApiReturnCode("获取已关注列表失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                        throw new Exception(string.Format("获取已关注列表失败。\r\nerrCode：{0}\r\n返回值说明：{1}\r\njson：{2}", returnCode.ErrCode, returnCode.Msg, returnCode.Json));
                    }
                }
                catch (PostHttpErrorException e)
                {
                    var msg = string.Format("获取已关注列表失败，HTTP 状态码{0}", e.HttpStatusCode);
                    LogHelper.Log(msg);
                    throw new Exception(msg);
                }
                catch (Exception e)
                {
                    var msg = string.Format("获取已关注列表失败。\r\n错误消息：{1}", e.Message);
                    LogHelper.LogError(e);
                    LogHelper.Log(msg, e);
                    throw new Exception(msg);
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
            string accessToken = null;
            try
            {
                accessToken = WeiXinAccessTokenManager.GetToken();
            }
            catch (Exception e)
            {
                LogHelper.Log("获取已关注用户信息失败", e);
                LogHelper.LogError(e);
                throw new Exception(string.Format("获取已关注用户信息失败。\r\n错误消息：\r\n{1}", e.Message));
            }
            try
            {
                var url = string.Format("{0}?access_token={1}&openid={2}&lang=zh_CN", ConfigProperty.WeiXin_GetUserInfoApi, accessToken, openId);
                var json = HttpRequestHelper.GetHttp_ForamtByJson(url);
                var returnCode = GlobalReturnCodeManager.GetReturnCode(json);
                if (returnCode.IsRequestSuccess)
                {
                    var userInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<UserInfo>(json);
                    return userInfo;
                }
                else
                {
                    LogHelper.LogWeiXinApiReturnCode("获取已关注用户信息失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                }
            }
            catch (PostHttpErrorException e)
            {
                var msg = string.Format("获取已关注用户信息失败，HTTP 状态码{0}", e.HttpStatusCode);
                LogHelper.Log(msg);
                throw new Exception(msg);
            }
            catch (Exception e)
            {
                var msg = string.Format("获取已关注用户信息失败。\r\n错误消息：{1}", e.Message);
                LogHelper.LogError(e);
                LogHelper.Log(msg, e);
                throw new Exception(msg);
            }
            throw new Exception("获取已关注用户信息失败");
        }
    }
}
