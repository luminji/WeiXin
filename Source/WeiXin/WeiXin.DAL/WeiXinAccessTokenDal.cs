using System;
using System.Data;
using System.Data.SqlClient;
using WeiXin.Models;
using WeiXin.Utilitys;

namespace WeiXin.DAL
{
    public class WeiXinAccessTokenDal : DalBase<WeiXinAccessToken>
    {
        public bool Save(WeiXinAccessToken model)
        {
            var result = default(bool);
            var list = base.Read();
            var sql = string.Empty;
            if (list != null && list.Count > 0)
            {
                try
                {
                    sql = "update WeiXinAccessTokenTest set AccessToken=@AccessToken,ExpiresIn=@ExpiresIn,LastGetDatetime=@LastGetDatetime;";
                    result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@AccessToken", model.AccessToken), new SqlParameter("@ExpiresIn", model.ExpiresIn), new SqlParameter("@LastGetDatetime", model.LastGetDatetime)) > 0;
                }
                catch (Exception e)
                {
                    LogHelper.LogError(e);
                }
            }
            else
            {
                result = base.Create(model);
            }
            return result;
        }
    }
}
