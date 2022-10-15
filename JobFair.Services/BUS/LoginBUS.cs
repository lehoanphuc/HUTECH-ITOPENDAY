using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class LoginBUS : BaseBUS
    {
        public ResultViewModel Login(LoginViewModel data)
        {
            var result = new ResultViewModel();

            // Check obj
            if (data is null)
            {
                return result;
            }

            // Check username and password
            var user = db.USERs.FirstOrDefault(x => x.Username == data.Username && x.IsDeleted != true);

            // Check Whether a User Exists
            if (user is null)
            {
                result.Message = "Sai tên tài khoản";
                return result;
            }

            // Check password
            var hashPassword = MD5Helper.Hash(data.Password);
            if (!hashPassword.Equals(user.Password, StringComparison.OrdinalIgnoreCase))
            {
                result.Message = "Sai mật khẩu";
                return result;
            }
            else
            {
                // Login success
                result.Data = user;
                result.Success = true;
            }

            return result;
        }
    }
}
