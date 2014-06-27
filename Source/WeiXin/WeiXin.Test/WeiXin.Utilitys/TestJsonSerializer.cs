using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Models;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Utilitys
{
    [TestClass]
    public class TestJsonSerializer
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = "{\"name\":\"wangwenzhuang\"}";
            var result = JsonSerializerHelper.Deserialize(json);
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(result.Count, 1);
            Assert.AreEqual<string>(result["name"].ToString(), "wangwenzhuang");
        }

        [TestMethod]
        public void TestDeserializes()
        {
            var json = "[{\"name\": \"wangwenzhuang\"},{\"name\": \"yhbj\"}]";
            var result = JsonSerializerHelper.Deserializes(json);
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(result.Count, 2);
            Assert.AreEqual<string>(result[1]["name"].ToString(), "yhbj");
        }

        [TestMethod]
        public void TestSerialize()
        {
            var obj = new { name = "wangwenzhuang" };
            var result = JsonSerializerHelper.Serialize(obj);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(result, "{\"name\":\"wangwenzhuang\"}");
        }

        [TestMethod]
        public void TestConvertJsonStringToObject()
        {
            var json = "{\"AccessToken\": \"ACCESS_TOKEN\",\"ExpiresIn\":7200}";
            var result = JsonSerializerHelper.ConvertJsonStringToObject<WeiXinAccessToken>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(result.AccessToken, "ACCESS_TOKEN");
            Assert.AreEqual<int>(result.ExpiresIn, 7200);
        }

        [TestMethod]
        public void TestConvertJsonStringToObjects()
        {
            var json = "[{\"AccessToken\": \"ACCESS_TOKEN1\",\"ExpiresIn\":7200},{\"AccessToken\": \"ACCESS_TOKEN2\",\"ExpiresIn\":7200}]";
            var result = JsonSerializerHelper.ConvertJsonStringToObjects<WeiXinAccessToken>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(result.Count, 2);
            Assert.AreEqual<string>(result[1].AccessToken, "ACCESS_TOKEN2");
        }
    }
}
