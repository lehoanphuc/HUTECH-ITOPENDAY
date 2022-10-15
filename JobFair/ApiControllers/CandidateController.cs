using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using WebPortal.Models.Attributes;

namespace JobFair.ApiControllers
{
    [BasicAuthentication(new int[] { (int)RoleKeys.ADMIN, (int)RoleKeys.COMPANY })]
    public class CandidateController : ApiController
    {
        public IEnumerable<CandidateViewModel> Get(int? idCompany, int? idJobTitle)
        {
            var username = string.Empty;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                username = HttpContext.Current.User.Identity.Name;
            }

            try
            {
                // Lấy setting
                bool allowCV = false;
                var settingBUS = new SettingBUS();
                try
                {
                    allowCV = settingBUS.GetValueByKey<bool>(SettingKeys.ALLOW_CV.ToString());
                }
                catch
                {
                    // Do nothing
                }

                using (var userBUS = new UserBUS())
                {
                    var userModel = userBUS.GetUserByUsername(username);

                    if (userModel.IdRole == (int)RoleKeys.ADMIN)
                    {
                        allowCV = true;
                    }
                    else
                    {
                        idCompany = userModel.IdCompany;
                    }
                }

                using (var bus = new CandidateBUS())
                {
                    return bus.GetList(idCompany, idJobTitle, allowCV);
                }
            }
            catch
            {
                return new List<CandidateViewModel>();
            }
        }

        [BasicAuthentication(new int[] { (int)RoleKeys.ADMIN })]
        [HttpGet]
        public ResultViewModel ToggleStatus()
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new SettingBUS())
                {
                    var allowCV = false;
                    try
                    {
                        allowCV = bus.GetValueByKey<bool>(SettingKeys.ALLOW_CV.ToString());
                    }
                    catch
                    {
                        // Do nothing
                    }

                    allowCV = !allowCV;
                    bus.Save(SettingKeys.ALLOW_CV.ToString(), allowCV.ToString());
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