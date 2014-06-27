using System;
using System.IO;
using System.Web;
using WeiXin.Config;
using WeiXin.Messages;
using WeiXin.ProcessMessages;
using WeiXin.Utilitys;

namespace WeiXin
{
    /// <summary>
    /// 微信公共平台接口
    /// </summary>
    public class WeiXin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            if (SignatureHelper.CheckSignature(ConfigProperty.WeiXin_Token, signature, timestamp, nonce))
            {
                var writeMsg = string.Empty;
                if ("post".Equals(context.Request.HttpMethod.ToLower()))
                {
                    string xml = null;
                    using (var reader = new StreamReader(context.Request.InputStream))
                    {
                        xml = HttpUtility.UrlDecode(reader.ReadToEnd());
                        LogHelper.LogWeiXinMessage(xml);
                    }
                    if (!string.IsNullOrEmpty(xml))
                    {
                        writeMsg = ProcessPost(xml);
                    }
                }
                else
                {
                    writeMsg = echoStr;
                }
                HttpContext.Current.Response.Write(writeMsg);
                HttpContext.Current.Response.End();
            }
        }

        private string ProcessPost(string xml)
        {
            string result = string.Empty;
            try
            {
                var msgType = MessageManager.GetMsgTypeByXml(xml);
                if (ProcessMessageManager.IsReply(msgType))
                {
                    var convertResult = MessageManager.ConvertToMessage(xml);
                    if (convertResult != null)
                    {
                        var msg = (Message)convertResult["Msg"];
                        result = ProcessMessageManager.Process(msg, msgType);
                    }
                    else
                    {
                        result = ProcessMessageManager.NotProcess(xml);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
            return result;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}