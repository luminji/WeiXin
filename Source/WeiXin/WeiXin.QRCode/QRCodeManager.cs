using System;
using System.Collections.Generic;
using WeiXin.AccessToken;
using WeiXin.Config;
using WeiXin.Utilitys;

namespace WeiXin.QRCode
{
    public sealed class QRCodeManager
    {
        public static Dictionary<string, object> CreateQR_SCENE(long scene_id)
        {
            var obj = new { expire_seconds = 1800, action_name = "QR_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            return CreateQR(obj);
        }

        public static Dictionary<string, object> CreateQR_LIMIT_SCENEE(long scene_id)
        {
            var obj = new { action_name = "QR_LIMIT_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            return CreateQR(obj);
        }

        private static Dictionary<string, object> CreateQR(object obj)
        {
            var accessToken = WeinXinAccessTokenManager.GetToken();
            var url = string.Format("{0}?access_token={1}", ConfigProperty.WeiXin_CreateQRApi, accessToken);
            try
            {
                var json = JsonSerializerHelper.Serialize(obj);
                var resultJson = HttpRequestHelper.PostHttp_ForamtByJson(url, json);
                var jsonObj = JsonSerializerHelper.Deserialize(resultJson);
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
