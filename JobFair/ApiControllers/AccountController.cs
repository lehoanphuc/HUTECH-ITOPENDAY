using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using WebPortal.Models.Attributes;

namespace JobFair.ApiControllers
{
    [BasicAuthentication(new int[] { (int)RoleKeys.ADMIN })]
    public class AccountController : ApiController
    {
        public IEnumerable<UserViewModel> GetCompany()
        {
            using (var bus = new UserBUS())
            {
                return bus.GetList((int)RoleKeys.COMPANY).Data;
            }
        }

        public PagedDataViewModel GetStudent(int page = 1, int itemPerPage = 50)
        {
            using (var bus = new UserBUS())
            {
                return bus.GetList((int)RoleKeys.STUDENT, page, itemPerPage);
            }
        }

        [HttpPost]
        public ResultViewModel Delete(int id)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new UserBUS())
                {
                    bus.Delete(id);
                }

                result.Message = "Xóa thành công tài khoản này";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public ResultViewModel SendPass(int id)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new UserBUS())
                {
                    var user = bus.Get(id);
                    if (string.IsNullOrEmpty(user.Email))
                    {
                        throw new Exception("Bạn chưa thiết lập email cho tài khoản này");
                    }

                    var newPass = bus.RandomPassword(id);
                    string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    string url = domainName + "/login";
                    string emailBody = $@"Kính gửi quý doanh nghiệp - {user.CompanyName},<br><br>
Ban tổ chức HUTECH IT Open Day xin phép gửi đến quý doanh nghiệp tài khoản truy cập vào trang quản lý ứng viên của ngày hội, dưới đây là tài khoản của quý doanh nghiệp<br><br>
- Tài khoản: {user.Username}<br>
- Mật khẩu: {newPass}<br><br>
Bấm vào link dưới đây để thực hiện thao tác đăng nhập:<br><br>
<a href='{url}'>{url}<a>";
                    EmailHelper.Send(user.Email, "[HUTECH IT OPEN DAY] Tài khoản truy cập trang quản lý ứng viên", emailBody);
                }

                result.Message = "Đã gửi mail thành công cho tài khoản này";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }


        [HttpPost]
        public ResultViewModel SaveCompany(UserViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                // Validate data
                ValidateData.Validate(data, _throw: true);

                using (var bus = new UserBUS())
                {
                    bus.SaveCompany(data);
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}