using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Attributes;
using WeiXin.Messages;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Utilitys
{
    [TestClass]
    public class TestReflectionHelp
    {
        [TestMethod]
        public void TestGetCustomAttribute()
        {
            var reflectionAttribute = ReflectionHelper.GetCustomAttribute<MessageReflectionAttribute>(MessageType.Text);
            Assert.IsNotNull(reflectionAttribute);
            Assert.IsTrue(reflectionAttribute is MessageReflectionAttribute);
        }

        [TestMethod]
        public void TestCreateObject()
        {
            var reflectionAttribute = ReflectionHelper.GetCustomAttribute<MessageReflectionAttribute>(MessageType.Text);
            var msg = ReflectionHelper.CreateObject(reflectionAttribute.AssemblyString, string.Format("{0}.{1}", reflectionAttribute.AssemblyString, reflectionAttribute.ClassName));
            Assert.IsNotNull(msg);
        }
    }
}
