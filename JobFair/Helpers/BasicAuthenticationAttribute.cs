using JobFair.Services.BUS;
using JobFair.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebPortal.Models.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly int[] allowedroles;

        public BasicAuthenticationAttribute(params int[] roles)
        {
            this.allowedroles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool checkLogin = (HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated;
            if (checkLogin)
            {
                if (!checkRole(HttpContext.Current.User.Identity.Name))
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                    return;
                }

                AddAuthorization(HttpContext.Current.User.Identity.Name);
                return;
            }

            //If the Authorization header is empty or null
            //then return Unauthorized
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                //Get the authentication token from the request header
                string authenticationToken = actionContext.Request.Headers
                    .Authorization.Parameter;
                //Decode the string
                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));
                //Convert the string into an string array
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                //First element of the array is the username
                string username = usernamePasswordArray[0];
                //Second element of the array is the password
                string password = usernamePasswordArray[1];
                //call the login method to check the username and password

                using (var bus = new LoginBUS())
                {
                    var result = bus.Login(new LoginViewModel
                    {
                        Username = username,
                        Password = password
                    });

                    if (result.Success)
                    {
                        if (!checkRole(username))
                        {
                            actionContext.Response = actionContext.Request
                                .CreateResponse(HttpStatusCode.Unauthorized);
                            return;
                        }

                        AddAuthorization(username);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
            }
        }

        public void AddAuthorization(string username)
        {
            var identity = new GenericIdentity(username);
            IPrincipal principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private bool checkRole(string username)
        {
            // check roles
            int userRole = -1;
            using (var bus = new UserBUS())
            {
                userRole = bus.GetRoleByUsername(username);
            }

            bool flag = true;

            if (allowedroles != null && allowedroles.Length > 0)
            {
                flag = false;

                foreach (var role in allowedroles)
                {
                    if (userRole == role)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag) return flag;
            }

            return flag;
        }
    }
}