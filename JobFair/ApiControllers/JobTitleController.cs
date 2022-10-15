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
    public class JobTitleController : ApiController
    {
        public IEnumerable<JobTitleViewModel> Get()
        {
            using (var bus = new JobTitleBUS())
            {
                return bus.GetList();
            }
        }

        /// <summary>
        /// Cái này khác cái company vì nó không có upload file by web api controller
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultViewModel Save(JobTitleViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new JobTitleBUS())
                {
                    bus.Save(data);
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