using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using System.Web.Mvc;

namespace JobFair.Controllers
{
    [UserAuthencation(new int[] { (int)RoleKeys.ADMIN, (int)RoleKeys.COMPANY })]
    public class CandidateController : Controller
    {
        /// <summary>
        /// Xem danh sách ứng viên
        /// </summary>
        /// <returns></returns>
        [Route("admin/candidate")]
        public ActionResult List()
        {
            var user = SessionHelper.GetUser();
            var bus = new CandidateBUS();
            ViewBag.Filter = bus.GetFilter(user.IdCompany);
            return View();
        }

        [Route("admin/stat-candidate")]
        public ActionResult Statistical()
        {
            var user = SessionHelper.GetUser();
            var bus = new CandidateBUS();

            // Convert to 2d array
            var statData = bus.GetStat(user.IdCompany);

            return View(statData);
        }

    }
}