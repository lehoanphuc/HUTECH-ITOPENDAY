using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JobFair.Helpers
{
    public class UserAuthencationAttribute : AuthorizeAttribute
    {
        private readonly int[] allowedroles;

        public UserAuthencationAttribute(params int[] roles)
        {
            this.allowedroles = roles;
        }

        public UserAuthencationAttribute()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                // Check user is login or not
                if (!SessionHelper.IsLogin())
                {
                    SessionHelper.ClearUser();
                    return false;
                }

                var user = SessionHelper.GetUser();
                List<int> userRoles = new List<int>();
                userRoles.Add(user.IdRole);

                if (allowedroles != null)
                {
                    // check roles
                    bool flag = false;
                    foreach (var role in allowedroles)
                    {
                        if (userRoles.Any(x => x == role))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag) return flag;
                }

                return true;
            }
            catch
            {
                SessionHelper.ClearUser();
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Check token is exist
            if (!SessionHelper.IsLogin())
            {
                filterContext.Controller.TempData["Error"] = "Phiên đăng nhập không hợp lệ";

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login", returnURL = HttpContext.Current.Request.Url.AbsoluteUri }));
                return;
            }

            filterContext.Controller.TempData["Error"] = "Bạn không có quyền truy cập chức năng này";
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login" }));
        }
    }
}