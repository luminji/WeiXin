using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.UserManager;
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
        public void TestConvertJsonStringToObjectByJsonPropertyAttribute()
        {
            var json = "{\"subscribe\":1,\"openid\":\"oxhAYuPP7QcvPBq33dXs9f8Kvo2Y\",\"nickname\":\"王文壮\",\"sex\":1,\"language\":\"zh_CN\",\"city\":\"白山\",\"province\":\"吉林\",\"country\":\"中国\",\"headimgurl\":\"http://wx.qlogo.cn/mmopen/cjmq2iaVRZDXFNCWxV27YyW0q0cqnbhTPDibkDibapkmutfYD4RiaG7aKN9oOztXG9SY2yB3roSComjIVE95S4RJOQ/0\",\"subscribe_time\":1403936603,\"remark\":\"\"}";
            var result = JsonSerializerHelper.ConvertJsonStringToObjectByJsonPropertyAttribute<UserInfo>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(result.NickName, "王文壮");
        }
    }
}
