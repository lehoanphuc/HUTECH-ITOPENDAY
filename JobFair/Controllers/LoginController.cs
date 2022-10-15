using JobFair.DomainModels;
using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace JobFair.Controllers
{
    public class LoginController : Controller
    {
        [Route("login")]
        public ActionResult Index()
        {
            // Kiểm tra error msg khi điều hướng
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"].ToString();
            }

            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Index(LoginViewModel data)
        {
            var resultLogin = DoLogin(data);
            var dataLogin = (ResultViewModel)resultLogin.Data;

            if (dataLogin.Success)
            {
                if (SessionHelper.GetUser().IdRole == (int)RoleKeys.ADMIN)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (SessionHelper.GetUser().IdRole == (int)RoleKeys.COMPANY)
                {
                    return RedirectToAction("List", "Candidate");
                }
            }
            else
            {
                ViewBag.Error = dataLogin.Message;
            }

            return View();
        }

        /// <summary>
        /// Helper for Login function
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult DoLogin(LoginViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(data.Token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                // Validate model
                ValidateData.Validate(data, _throw: true);

                // Check login
                using (LoginBUS bus = new LoginBUS())
                {
                    result = bus.Login(data);

                    // Check login success
                    if (result.Success)
                    {
                        // Check role student
                        if (data.Student)
                        {
                            var userObj = (USER)result.Data;
                            if (userObj.IdRole != (int)RoleKeys.STUDENT)
                            {
                                throw new Exception("Cổng đăng nhập này chỉ dành cho sinh viên");
                            }
                        }

                        SessionHelper.SaveUser(result.Data);
                        FormsAuthentication.SetAuthCookie(data.Username, false);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            result.Data = null;
            return Json(result);
        }

        /// <summary>
        /// Sign out
        /// </summary>
        /// <returns></returns>
        [UserAuthencation]
        [Route("logout")]
        public ActionResult Logout()
        {
            SessionHelper.ClearUser();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sign out for student
        /// </summary>
        /// <returns></returns>
        [UserAuthencation]
        [Route("student/logout")]
        public ActionResult StudentLogout()
        {
            SessionHelper.ClearUser();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }       
    }
}