using System;
using System.Threading.Tasks;
using WeiXin.AccessToken;
using WeiXin.Config;
using WeiXin.GlobalReturnCode;
using WeiXin.Utilitys;

namespace WeiXin.Mass
{
    /// <summary>
    /// 高级群发接口，根据 OpenId
    /// 获取已关注用户列表，请使用 WeiXin.UserManager.WeiXinUserManager.GetUserList() 方法
    /// </summary>
    public sealed class MassManager
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="msg"></param>
        public static void SendTextMessage(MassTextMessage msg)
        {
            SendMessage(msg.GetJson());
        }
        private static void SendMessage(string json)
        {
            Task t = new Task(() =>
            {
                string accessToken = null;
                try
                {
                    accessToken = WeiXinAccessTokenManager.GetToken();
                }
                catch (Exception e)
                {
                    LogHelper.Log(string.Format("发送群发消息失败。\r\n错误消息：\r\n{1}", e.Message));
                    LogHelper.LogError(e);
                    return;
                }
                string api = string.Format("{0}?access_token={1}", ConfigProperty.WeiXin_AdvancedMassApi, accessToken);
                LogHelper.LogWeiXinMessage(json);
                try
                {
                    LogHelper.LogWeiXinMessage(json);
                    var resultJson = HttpRequestHelper.PostHttp_ForamtByJson(api, json);
                    var returnCode = GlobalReturnCodeManager.GetReturnCode(resultJson);
                    if (!returnCode.IsRequestSuccess)
                    {
                        LogHelper.LogWeiXinApiReturnCode("发送群发消息失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                    }
                }
                catch (PostHttpErrorException e)
                {
                    LogHelper.Log(string.Format("发送群发消息失败，HTTP 状态码{0}，JSON 数据：\r\n{1}", e.HttpStatusCode, json));
                }
                catch (Exception e)
                {
                    LogHelper.LogError(e);
                    LogHelper.Log(string.Format("发送群发消息失败，JSON 数据：\r\n{0}\r\n错误消息：\r\n{1}", json, e.Message));
                }
            });
            t.Start();
        }
    }
}
