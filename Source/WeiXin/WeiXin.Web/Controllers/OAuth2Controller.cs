using System.Web.Mvc;
using WeiXin.OAuth2;

namespace WeiXin.Web.Controllers
{
    public class OAuth2Controller : Controller
    {
        //
        // GET: /OAuth2/
        public ActionResult Index(string state, string code = null)
        {
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(code))
            {
                var token = WeiXinOAuth2Manager.GetToken(code);
                ViewBag.Code = code;
                ViewBag.OpenId = token.OpenId;
                var isGetUserInfo = state.Equals("1");
                @ViewBag.IsUserInfo = isGetUserInfo;
                if (state.Equals("1"))
                {
                    ViewBag.UserInfo = WeiXinOAuth2Manager.GetUserInfo(token.OpenId, token.AccessToken);
                }
            }
            return View();
        }
    }
}