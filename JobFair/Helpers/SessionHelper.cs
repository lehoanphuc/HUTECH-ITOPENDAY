using JobFair.DomainModels;
using JobFair.Shared.Constants;
using System.Web;

namespace JobFair.Helpers
{
    public class SessionHelper
    {
        /// <summary>
        /// Lưu thông tin tài khoản
        /// </summary>
        /// <param name="data"></param>
        public static void SaveUser(USER data)
        {
            HttpContext.Current.Session.Add(SessionKeys.USER_OBJ.ToString(), data);
        }

        /// <summary>
        /// Clear toàn bộ session user để logout
        /// </summary>
        public static void ClearUser()
        {
            HttpContext.Current.Session.Remove(SessionKeys.USER_OBJ.ToString());
        }

        /// <summary>
        /// Lấy thông tin user đang đăng nhập
        /// </summary>
        /// <returns></returns>
        public static USER GetUser()
        {
            if (HttpContext.Current.Session[SessionKeys.USER_OBJ.ToString()] != null)
            {
                return (USER)HttpContext.Current.Session[SessionKeys.USER_OBJ.ToString()];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra phiên đăng nhập hiện tại hợp lệ không
        /// </summary>
        public static bool IsLogin()
        {
            try
            {
                var user = GetUser();
                return user != null;
            }
            catch
            {
                return false;
            }
        }
    }
}