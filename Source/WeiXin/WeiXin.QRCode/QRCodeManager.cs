using System;
using System.Collections.Generic;
using WeiXin.AccessToken;
using WeiXin.Config;
using WeiXin.GlobalReturnCode;
using WeiXin.Utilitys;

namespace WeiXin.QRCode
{
    public sealed class QRCodeManager
    {
        /// <summary>
        /// 创建临时带参数二维码
        /// </summary>
        /// <param name="scene_id">参数值</param>
        /// <returns></returns>
        public static QRCodeInfo CreateQR_SCENE(long scene_id)
        {
            var obj = new { expire_seconds = 1800, action_name = "QR_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            return CreateQR(obj);
        }

        /// <summary>
        /// 创建永久带参数二维码
        /// </summary>
        /// <param name="scene_id">参数值</param>
        /// <returns></returns>
        public static QRCodeInfo CreateQR_LIMIT_SCEN(long scene_id)
        {
            var obj = new { action_name = "QR_LIMIT_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            return CreateQR(obj);
        }

        private static QRCodeInfo CreateQR(object obj)
        {
            string accessToken = null;
            try
            {
                accessToken = WeiXinAccessTokenManager.GetToken();
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
                LogHelper.Log("创建二维码失败", e);
                throw new Exception(string.Format("创建二维码失败。\r\n错误消息：\r\n{1}", e.Message));
            }
            try
            {
                var url = string.Format("{0}?access_token={1}", ConfigProperty.WeiXin_CreateQRApi, accessToken);
                var json = JsonSerializerHelper.Serialize(obj);
                var resultJson = HttpRequestHelper.PostHttp_ForamtByJson(url, json);
                var returnCode = GlobalReturnCodeManager.GetReturnCode(resultJson);
                if (returnCode.IsRequestSuccess)
                {
                    var jsonObj = JsonSerializerHelper.Deserialize(resultJson);
                    var qrCodeInfo = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<QRCodeInfo>(json);
                    return qrCodeInfo;
                }
                else
                {
                    LogHelper.LogWeiXinApiReturnCode("创建二维码失败", returnCode.ErrCode, returnCode.Msg, returnCode.Json);
                }
            }
            catch (PostHttpErrorException e)
            {
                var msg = string.Format("创建二维码失败，HTTP 状态码{0}", e.HttpStatusCode);
                LogHelper.Log(msg);
                throw new Exception(msg);
            }
            catch (Exception e)
            {
                var msg = string.Format("创建二维码失败。\r\n错误消息：{1}", e.Message);
                LogHelper.LogError(e);
                LogHelper.Log(msg, e);
                throw new Exception(msg);
            }
            throw new Exception("创建二维码失败");
        }
    }
}
