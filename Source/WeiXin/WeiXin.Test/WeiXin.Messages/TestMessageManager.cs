using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Messages;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Messages
{
    [TestClass]
    public class TestMessageManager
    {
        static string xml = "<xml><ToUserName><![CDATA[gh_79b66d910e07]]></ToUserName>" +
"<FromUserName><![CDATA[oFiw3uKADZmZP5FELn-8tmqhQIPI]]></FromUserName>" +
"<CreateTime>1398157276</CreateTime>" +
"<MsgType><![CDATA[text]]></MsgType>" +
"<Content><![CDATA[4]]></Content>" +
"<MsgId>6005039775284630226</MsgId>" +
"</xml>";

        [TestMethod]
        public void TestConvertToMessage()
        {
            var result = MessageManager.ConvertToMessage(xml);
            var msg = (Message)result["Msg"];
            Assert.IsNotNull(result);
            Assert.IsNotNull(msg);
            Assert.IsTrue(((MessageType)result["MsgType"]) == MessageType.Text);
        }

        [TestMethod]
        public void TestGetMsgTypeByXml()
        {
            var result = MessageManager.GetMsgTypeByXml(xml);
            Assert.IsTrue(result == MessageType.Text);
        }

        [TestMethod]
        public void TestCreateTextMessageXml()
        {
            var result = MessageManager.CreateTextMessageXml(xml, "wangwenzhuang");
            var values = XmlHelper.GetElementValue(result, "Content");
            Assert.IsNotNull(values);
            Assert.AreEqual<int>(values.Length, 1);
            Assert.AreEqual<string>(values[0], "wangwenzhuang");
        }
    }
}
