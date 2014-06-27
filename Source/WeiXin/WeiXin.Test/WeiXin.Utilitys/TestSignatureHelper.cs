using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Utilitys
{
    [TestClass]
    public class TestSignatureHelper
    {
        /*
         * 测试签名功能，其中包含的方法
         * CreateSignature
         * CreateTimestamp
         * CreateRandomNumber
         * CheckSignature
         */
        [TestMethod]
        public void TestRead()
        {
            var token = "wangwenzhuang";
            var timestamp = SignatureHelper.CreateTimestamp();
            var nonce = SignatureHelper.CreateRandomNumber();
            var signature = SignatureHelper.CreateSignature(new string[] { token, timestamp, nonce });
            var result = SignatureHelper.CheckSignature(token, signature, timestamp, nonce);
            Assert.IsTrue(result);
        }
    }
}
