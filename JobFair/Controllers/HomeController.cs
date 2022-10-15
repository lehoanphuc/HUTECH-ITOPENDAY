using JobFair.Services.BUS;
using System.Web.Mvc;

namespace JobFair.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Trang hội thảo
        /// </summary>
        /// <returns></returns>
        [Route("event")]
        public ActionResult Event()
        {
            return View();
        }

        /// <summary>
        /// Trang học bổng
        /// </summary>
        /// <returns></returns>
        [Route("scholarship")]
        public ActionResult Scholarship()
        {
            return View();
        }


        /// <summary>
        /// Trang interview
        /// </summary>
        /// <returns></returns>
        [Route("interview")]
        public ActionResult Interview()
        {
            return View();
        }

        /// <summary>
        /// Tìm việc
        /// </summary>
        /// <returns></returns>
        [Route("job")]
        public ActionResult FindJob()
        {
            return View();
        }

        [Route("company/{name}")]
        public ActionResult Company(string name)
        {
            try
            {
                // Find company by name
                using (var bus = new CompanyBUS())
                {
                    var company = bus.GetByName(name);
                    return View(company);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            } 
        }

        [Route("resetpassword/{token}")]
        public ActionResult ResetPassword(string token)
        {
            using (var bus = new UserBUS())
            {
                if (!bus.CheckToken(token))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Token = token;
            return View();
        }
    }
}