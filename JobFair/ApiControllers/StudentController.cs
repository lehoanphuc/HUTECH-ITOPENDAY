using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.Shared.MasterData;
using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebPortal.Models.Attributes;

namespace JobFair.ApiControllers
{
    /// <summary>
    /// This API Controller is using for STUDENT SIDE
    /// </summary>
    public class StudentController : ApiController
    {
        [HttpPost]
        public ResultViewModel RegEvent(StudentRegEventViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                // Validate data
                ValidateData.Validate(data, _throw: true);

                // Save data
                using (var bus = new EventBUS())
                {
                    bus.Reg(data);
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

        [HttpPost]
        public ResultViewModel Scholarship(string token)
        {
            var result = new ResultViewModel();

            try
            {
                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                using (var settingBUS = new SettingBUS())
                {
                    // Check setting
                    var allowReg = false;
                    try
                    {
                        allowReg = settingBUS.GetValueByKey<bool>(SettingKeys.ALLOW_SCHOLARSHIP.ToString());
                    }
                    catch
                    {
                        // Do nothing
                    }

                    if (!allowReg)
                    {
                        throw new Exception("Cổng đăng ký đã đóng, vui lòng trở lại sau");
                    }
                }

                var httpRequest = HttpContext.Current.Request;

                // Parse form data to model
                var data = new StudentRegScholarshipViewModel(httpRequest.Form);

                // Check files
                if (httpRequest.Files.Count != 2) throw new Exception("Vui lòng upload CV và bảng điểm của bạn");
                HttpPostedFileBase filePoint = new HttpPostedFileWrapper(httpRequest.Files["file1"]);
                if (string.IsNullOrEmpty(filePoint.FileName)) throw new Exception("Vui lòng upload bảng điểm của bạn");

                HttpPostedFileBase fileCV = new HttpPostedFileWrapper(httpRequest.Files["file2"]);
                if (string.IsNullOrEmpty(fileCV.FileName)) throw new Exception("Vui lòng upload CV của bạn");

                // Validate data
                ValidateData.Validate(data, _throw: true);

                // Save data
                int idReg = 0;
                using (var bus = new ScholarshipBUS())
                {
                    idReg = bus.Reg(data);
                }

                // Upload files
                var fileType = filePoint.FileName.Split('.').Last();
                UploadImageHelper.Upload(filePoint, idReg + "." + fileType,
                    WebSettingData.LinkScholarshipPoint,
                    WebSettingData.UploadFilePointTypes);

                UploadImageHelper.Upload(fileCV, idReg + ".pdf",
                    WebSettingData.LinkScholarshipCV,
                    new List<string>() { ".pdf" });

                result.Success = true;
                result.Message = "Đăng ký thành công, vui lòng chờ kết quả xét duyệt\nTheo dõi các thông tin mới nhất tại Fanpage Khoa CNTT - HUTECH";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public ResultViewModel RegUser(string token)
        {
            var result = new ResultViewModel();

            try
            {
                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                var httpRequest = HttpContext.Current.Request;

                // Parse form data to model
                var data = new StudentUserViewModel(httpRequest.Form);

                // Validate data
                ValidateData.Validate(data, _throw: true);

                // Check username
                long checkIDNumber = 0;
                if (!long.TryParse(data.Username, out checkIDNumber))
                {
                    throw new Exception("Mã số sinh viên hoặc số CMND không hợp lệ");
                }

                // Check password
                if (string.IsNullOrEmpty(data.Password))
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }

                // Check file
                if (httpRequest.Files.Count != 1) throw new Exception("Vui lòng upload CV của bạn");
                HttpPostedFileBase filebase = new HttpPostedFileWrapper(httpRequest.Files[0]);
                if (string.IsNullOrEmpty(filebase.FileName)) throw new Exception("Vui lòng upload CV của bạn");

                // Save data
                int idUser = 0;
                using (var bus = new UserBUS())
                {
                    idUser = bus.SaveStudent(data);
                }

                // Upload CV
                UploadImageHelper.Upload(filebase, idUser + ".pdf",
                    WebSettingData.LinkCV,
                    new List<string>() { ".pdf" });

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
        public IEnumerable<CompanyJobViewModel> FindJob(FindJobViewModel data)
        {
            var username = string.Empty;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                username = HttpContext.Current.User.Identity.Name;
            }

            try
            {
                // Convert some data to purle
                if (data is null)
                {
                    data = new FindJobViewModel();
                }

                if (data.FilterCompanyIDs is null)
                {
                    data.FilterCompanyIDs = new List<int>();
                }

                if (data.FilterJobTitleIDs is null)
                {
                    data.FilterJobTitleIDs = new List<int>();
                }

                using (var bus = new FindJobBUS())
                {
                    return bus.GetList(username, data.FilterJobTitleIDs, data.FilterCompanyIDs);
                }
            }
            catch
            {
                return new List<CompanyJobViewModel>();
            }
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        public ResultViewModel SubmitCV(int idCompany, int idJobTitle)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                using (var bus = new FindJobBUS())
                {
                    bus.SubmitCV(username, idCompany, idJobTitle);
                }
                result.Success = true;
                result.Message = "Đăng ký ứng tuyển thành công";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        public ResultViewModel SubmitCVAll(FindJobViewModel data)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                // Convert some data to purle
                if (data is null)
                {
                    data = new FindJobViewModel();
                }

                if (data.FilterCompanyIDs is null)
                {
                    data.FilterCompanyIDs = new List<int>();
                }

                if (data.FilterJobTitleIDs is null)
                {
                    data.FilterJobTitleIDs = new List<int>();
                }

                int count = 0;
                using (var bus = new FindJobBUS())
                {
                    var listJob = bus.GetList(username, data.FilterJobTitleIDs, data.FilterCompanyIDs);
                    foreach (var job in listJob.Where(x => x.IsApplied == false))
                    {
                        bus.SubmitCV(username, job.IdCompany, job.IdJobTitle);
                        count++;
                    }
                }

                result.Success = true;
                result.Message = $"Đăng ký ứng tuyển thành công {count} vị trí tuyển dụng";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        public ResultViewModel CancelCV(int idCompany, int idJobTitle)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                using (var bus = new FindJobBUS())
                {
                    bus.RemoveCV(username, idCompany, idJobTitle);
                }
                result.Success = true;
                result.Message = "Hủy đăng ký ứng tuyển thành công";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        public ResultViewModel Interview(int idCompany)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                using (var bus = new FindJobBUS())
                {
                    var link = bus.GetInterview(username, idCompany);

                    if (string.IsNullOrEmpty(link))
                    {
                        throw new Exception("Công ty này chưa có phòng phỏng vấn online, vui lòng liên hệ Khoa CNTT HUTECH để biết thêm thông tin chi tiết");
                    }

                    result.Data = link;
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

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        public ResultViewModel UpdateCV(string token)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                int idUser = -1;
                using (var bus = new UserBUS())
                {
                    var user = bus.GetUserByUsername(username);
                    idUser = user.Id;
                }


                // Check file
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count != 1) throw new Exception("Vui lòng upload CV của bạn");
                HttpPostedFileBase filebase = new HttpPostedFileWrapper(httpRequest.Files[0]);
                if (string.IsNullOrEmpty(filebase.FileName)) throw new Exception("Vui lòng upload CV của bạn");

                // Upload CV
                UploadImageHelper.Upload(filebase, idUser + ".pdf",
                    WebSettingData.LinkCV,
                    new List<string>() { ".pdf" });

                result.Message = "CV của bạn đã được cập nhật thành công";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.STUDENT })]
        [HttpPost]
        public ResultViewModel ChangePassword(ChangePasswordViewModel data)
        {
            var username = HttpContext.Current.User.Identity.Name;
            var result = new ResultViewModel();

            try
            {
                if (string.IsNullOrEmpty(data.CurrentPassword) || 
                    string.IsNullOrEmpty(data.Password) || 
                    string.IsNullOrEmpty(data.Repassword) ||
                    string.IsNullOrEmpty(data.Token))
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin");
                }

                if (!data.Password.Equals(data.Repassword))
                {
                    throw new Exception("Mật khẩu mới và xác nhận lại mật khẩu mới không trùng khớp");
                }

                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(data.Token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                using (var bus = new UserBUS())
                {
                    bus.ChangePassword(username, data.CurrentPassword, data.Password);
                }

                result.Message = "Lưu mật khẩu thành công, bạn có thể dùng mật khẩu mới để đăng nhập";
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
        public ResultViewModel ForgotPassword(ForgotPasswordViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                ValidateData.Validate(data, _throw: true);

                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(data.Token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                string token = string.Empty;
                using (var bus = new UserBUS())
                {
                    token = bus.ResetPassword(data.Username, data.Email);
                }

                string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                string url = domainName + "/resetpassword/" + token;
                string emailBody = $@"Xin chào bạn,<br><br>
Bạn vừa yêu cầu khôi phục mật khẩu cho tài khoản {data.Username}, nếu không phải là bạn yêu cầu, vui lòng bỏ qua email này<br><br>
Bấm vào link dưới đây để tiến hành khôi phục mật khẩu:<br><br>
<a href='{url}'>{url}<a>";
                EmailHelper.Send(data.Email, "[HUTECH IT OPEN DAY] Khôi phục mật khẩu", emailBody);

                result.Message = "Vui lòng kiểm tra email để tiến hành khôi phục mật khẩu";
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
        public ResultViewModel ResetPassword(ChangePasswordViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                if (string.IsNullOrEmpty(data.Password) || string.IsNullOrEmpty(data.Repassword) ||
                    string.IsNullOrEmpty(data.Token))
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin");
                }

                if (!data.Password.Equals(data.Repassword))
                {
                    throw new Exception("Mật khẩu mới và xác nhận lại mật khẩu mới không trùng khớp");
                }

                // Check captcha
                if (!RecaptchaHelper.ReCaptchaPassed(data.Token))
                {
                    throw new Exception("Lỗi xác thực ReCaptcha, vui lòng refesh lại page trước khi thao tác");
                }

                using (var bus = new UserBUS())
                {
                    bus.SaveResetPassword(data.UserToken, data.Password);
                }

                result.Message = "Lưu mật khẩu thành công, bạn có thể dùng mật khẩu mới để đăng nhập";
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