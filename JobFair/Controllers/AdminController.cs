using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using System.Web.Mvc;

namespace JobFair.Controllers
{
    [UserAuthencation(new int[] { (int)RoleKeys.ADMIN })]
    public class AdminController : Controller
    {
        [Route("admin")]
        public ActionResult Index()
        {
            return RedirectToAction("Company");
        }

        #region company
        [Route("admin/company")]
        public ActionResult Company()
        {
            return View();
        }

        [Route("admin/newcompany")]
        public ActionResult NewCompany()
        {
            return View();
        }

        [Route("admin/company/{id}")]
        public ActionResult CompanyInfo(int id)
        {
            try
            {
                using (var bus = new CompanyBUS())
                {
                    var model = bus.Get(id);
                    return View(model);
                }
            }
            catch
            {
                // Any error
                // I'm so lazy
                return RedirectToAction("Company");
            }
        }
        #endregion

        #region job title
        [Route("admin/jobtitle")]
        public ActionResult JobTitle()
        {
            return View();
        }
        #endregion

        #region event
        [Route("admin/event")]
        public ActionResult Event()
        {
            return View();
        }

        [Route("admin/newevent")]
        public ActionResult NewEvent()
        {
            return View();
        }

        [Route("admin/event/{id}")]
        public ActionResult EventInfo(int id)
        {
            try
            {
                using (var bus = new EventBUS())
                {
                    var model = bus.Get(id);
                    return View(model);
                }
            }
            catch
            {
                // Any error
                // I'm so lazy
                return RedirectToAction("Event");
            }
        }

        [Route("admin/event/student/{id}")]
        public ActionResult EventStudent(int id)
        {
            try
            {
                using (var bus = new EventBUS())
                {
                    // Check event does exist or not
                    var model = bus.Get(id);
                    ViewBag.ID = id;
                    return View();
                }
            }
            catch
            {
                // Any error
                // I'm so lazy
                return RedirectToAction("Event");
            }
        }
        #endregion

        #region Học bổng
        [Route("admin/scholarship")]
        public ActionResult Scholarship()
        {
            return View();
        }
        #endregion

        #region User company
        [Route("admin/usercompany")]
        public ActionResult UserCompany()
        {
            return View();
        }

        [Route("admin/newusercompany")]
        public ActionResult NewUserCompany()
        {
            return View();
        }

        [Route("admin/viewusercompany/{id}")]
        public ActionResult ViewUserCompany(int id)
        {
            try
            {
                using (var bus = new UserBUS())
                {
                    var model = bus.Get(id);
                    return View(model);
                }
            }
            catch
            {
                // Any error
                // I'm so lazy
                return RedirectToAction("UserCompany");
            }
        }
        #endregion

        #region User company
        [Route("admin/userstudent")]
        public ActionResult UserStudent()
        {
            return View();
        }
        #endregion
    }
}