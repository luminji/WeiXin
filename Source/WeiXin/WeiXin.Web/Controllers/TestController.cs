using System.Web.Mvc;

namespace WeiXin.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index(string id)
        {
            ViewBag.OpenId = id;
            return View();
        }
    }
}