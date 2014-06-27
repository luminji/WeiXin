using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Utilitys
{
    [TestClass]
    public class TestXmlHelp
    {
        [TestMethod]
        public void TestRead()
        {
            var xml = "<xml><name>wangwenzhuang</name></xml>";
            var result = XmlHelper.Read(xml);
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(result.Count, 1);
            Assert.AreEqual<string>(result["name"], "wangwenzhuang");
        }

        [TestMethod]
        public void TestGetElementValue()
        {
            var xml = "<xml><name>wangwenzhuang</name><age>25</age></xml>";
            var result = XmlHelper.GetElementValue(xml, "name");
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(result.Length, 1);
            Assert.AreEqual<string>(result[0], "wangwenzhuang");
        }
    }
}
