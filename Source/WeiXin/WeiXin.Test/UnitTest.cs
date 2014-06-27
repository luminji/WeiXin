using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using WeiXin.Utilitys;
using System.Threading;
using WeiXin.Messages;

namespace WeiXin.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            string xml = "<xml><ToUserName><![CDATA[gh_79b66d910e07]]></ToUserName>" +
"<FromUserName><![CDATA[oFiw3uKADZmZP5FELn-8tmqhQIPI]]></FromUserName>" +
"<CreateTime>1398157276</CreateTime>" +
"<MsgType><![CDATA[text]]></MsgType>" +
"<Content><![CDATA[4]]></Content>" +
"<MsgId>6005039775284630226</MsgId>" +
"</xml>";
            MessageManager.CreateNewsMessageXml(xml, null);
        }
    }
}
