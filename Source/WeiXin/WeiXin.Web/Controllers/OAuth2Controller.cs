using System.Web.Mvc;
using WeiXin.OAuth2AccessToken;

namespace WeiXin.Web.Controllers
{
    public class OAuth2Controller : Controller
    {
        //
        // GET: /OAuth2/
        public ActionResult Index(string state, string code = null)
        {
            if (!string.IsNullOrEmpty(code))
            {
                ViewBag.Code = code;
                ViewBag.OpenId = WeinXinOAuth2AccessTokenManager.GetOpenId(code);
            }
            return View();
        }
    }
}