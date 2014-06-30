using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.SendCustomerServiceMessage;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.SendMessage
{
    [TestClass]
    public class TestCustomerServiceTextMessage
    {
        /// <summary>
        /// 测试文本客服消息 json 数据格式
        /// </summary>
        [TestMethod]
        public void TestGetJson()
        {
            var msg = new CustomerServiceTextMessage();
            msg.Touser = "wangwenzhuang";
            msg.Content = "Hello";
            var resultJson = msg.GetJson();
            var result = JsonSerializerHelper.Deserialize(resultJson);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(result["touser"].ToString(), "wangwenzhuang");
        }
    }
}
