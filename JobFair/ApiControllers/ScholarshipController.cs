using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebPortal.Models.Attributes;

namespace JobFair.ApiControllers
{
    [BasicAuthentication(new int[] { (int)RoleKeys.ADMIN })]
    public class ScholarshipController : ApiController
    {
        public IEnumerable<StudentRegScholarshipViewModel> Get()
        {
            using (var bus = new ScholarshipBUS())
            {
                return bus.GetList();
            }
        }

        [HttpPost]
        public ResultViewModel Delete(int id)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new ScholarshipBUS())
                {
                    bus.Delete(id);
                }

                result.Message = "Xóa thành công dữ liệu đăng ký của sinh viên này";
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
                        allowReg = bus.GetValueByKey<bool>(SettingKeys.ALLOW_SCHOLARSHIP.ToString());
                    }
                    catch
                    {
                        // Do nothing
                    }

                    allowReg = !allowReg;
                    bus.Save(SettingKeys.ALLOW_SCHOLARSHIP.ToString(), allowReg.ToString());
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