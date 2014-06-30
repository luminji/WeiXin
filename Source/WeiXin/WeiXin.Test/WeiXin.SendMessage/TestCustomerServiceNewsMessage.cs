using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WeiXin.SendCustomerServiceMessage;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.SendMessage
{
    [TestClass]
    public class TestCustomerServiceNewsMessage
    {
        /// <summary>
        /// 测试图文消息 json 数据格式
        /// </summary>
        [TestMethod]
        public void TestGetJson()
        {
            var msg = new CustomerServiceNewsMessage();
            msg.Touser = "wangwenzhuang";
            msg.Articles = new List<Article>();
            msg.Articles.Add(new Article { Title = "标题", Description = "描述", Url = "http://www.baidu.com/" });
            var resultJson = msg.GetJson();
            var result = JsonSerializerHelper.Deserialize(resultJson);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(result["touser"].ToString(), "wangwenzhuang");
        }
    }
}
