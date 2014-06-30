using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Mass;
using WeiXin.UserManager;

namespace WeiXin.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var obj = WeiXinUserManager.GetUserInfo("oxhAYuPP7QcvPBq33dXs9f8Kvo2Y");
        }
    }
}
