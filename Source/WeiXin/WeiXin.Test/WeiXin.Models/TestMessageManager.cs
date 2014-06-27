using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using WeiXin.Messages;
using WeiXin.Models;
using WeiXin.Utilitys;

namespace WeiXin.Test.WeiXin.Models
{
    [TestClass]
    public class TestConvertModel
    {
        [TestMethod]
        public void TestConvert()
        {
            var dt = new DataTable();
            dt.Columns.Add("AccessToken", typeof(string));
            dt.Columns.Add("ExpiresIn", typeof(int));
            dt.Columns.Add("LastGetDatetime", typeof(DateTime));
            var row = dt.NewRow();
            row["AccessToken"] = "ACCESS_TOKEN";
            row["ExpiresIn"] = 7200;
            row["LastGetDatetime"] = DateTime.Now;
            dt.Rows.Add(row);
            var model = ConvertModel.Convert<WeiXinAccessToken>(dt);
            if (model != null)
            {
                Assert.IsNotNull(model);
                Assert.IsTrue(model is WeiXinAccessToken);
            }
        }
    }
}
