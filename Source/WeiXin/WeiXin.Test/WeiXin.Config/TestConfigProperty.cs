using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Config;

namespace WeiXin.Test.WeiXin.Config
{
    [TestClass]
    public class TestConfigProperty
    {
        [TestMethod]
        public void TestIsConfigurationOk()
        {
            Assert.IsTrue(ConfigProperty.IsConfigurationOk);
        }
    }
}
