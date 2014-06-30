using System;
using System.Threading.Tasks;
using WeiXin.AccessToken;
using WeiXin.Config;
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
        public static void SendMassTextMessage(MassTextMessage msg)
        {
            SendMassMessage(msg.GetJson());
        }
        private static void SendMassMessage(string json)
        {
            Task t = new Task(() =>
            {
                string token = WeinXinAccessTokenManager.GetToken();
                string api = string.Format("{0}?access_token={1}", ConfigProperty.WeiXin_AdvancedMassApi, token);
                LogHelper.LogWeiXinMessage(json);
                try
                {
                    var result = HttpRequestHelper.PostHttp_ForamtByJson(api, json);
                    if (!string.IsNullOrEmpty(result))
                    {
                        var jsonObj = JsonSerializerHelper.Deserialize(result);
                        if (jsonObj.ContainsKey("errcode"))
                        {
                            var errcode = Convert.ToInt32(jsonObj["errcode"]);
                            if (errcode > 0)
                            {
                                throw new Exception("result");
                            }
                        }
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
