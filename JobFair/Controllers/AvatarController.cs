using System.Web.Mvc;

namespace JobFair.Controllers
{
    public class AvatarController : Controller
    {
        [Route("avatar")]
        public ActionResult Index()
        {
            return View();
        }
    }
}