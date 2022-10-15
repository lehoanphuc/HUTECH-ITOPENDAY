using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.Shared.MasterData;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using WebPortal.Models.Attributes;

namespace JobFair.ApiControllers
{
    [BasicAuthentication(new int[] { (int)RoleKeys.ADMIN })]
    public class CompanyController : ApiController
    {
        public IEnumerable<CompanyViewModel> Get()
        {
            using (var bus = new CompanyBUS())
            {
                return bus.GetList();
            }
        }

        /// <summary>
        /// Xem thêm tại
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/sending-html-form-data-part-2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultViewModel Save()
        {
            var result = new ResultViewModel();

            try
            {
                var httpRequest = HttpContext.Current.Request;

                // Parse form data to model
                var data = new CompanyViewModel(httpRequest.Form);

                // Save db
                int id = 0;
                using (var bus = new CompanyBUS())
                {
                    id = bus.Save(data);
                }

                // Check files
                if (httpRequest.Files.Count > 0)
                {
                    // Save logo
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(httpRequest.Files[0]);
                    if (!string.IsNullOrEmpty(filebase.FileName))
                    {
                        UploadImageHelper.Upload(filebase, id + ".png",
                            WebSettingData.LinkLogoCompany,
                            new List<string>() { ".png" });
                    }
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

        [HttpGet]
        public ResultViewModel ToggleStatus()
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new SettingBUS())
                {
                    var allowReg = false;
                    try
                    {
                        allowReg = bus.GetValueByKey<bool>(SettingKeys.ALLOW_INTERVIEW.ToString());
                    }
                    catch
                    {
                        // Do nothing
                    }

                    allowReg = !allowReg;
                    bus.Save(SettingKeys.ALLOW_INTERVIEW.ToString(), allowReg.ToString());
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