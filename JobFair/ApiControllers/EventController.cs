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
    public class EventController : ApiController
    {
        public IEnumerable<StudentRegEventViewModel> GetStudentJoinEvent(int id)
        {
            using (var bus = new EventBUS())
            {
                return bus.GetListStudent(id);
            }
        }

        public IEnumerable<EventViewModel> Get()
        {
            using (var bus = new EventBUS())
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
                using (var bus = new EventBUS())
                {
                    bus.Delete(id);
                }

                result.Message = "Xóa thành công dữ liệu hội thảo này";
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
        public ResultViewModel ToggleStatus(int id)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new EventBUS())
                {
                    bus.ToggleStatusEvent(id);
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
        public ResultViewModel Save(EventViewModel data)
        {
            var result = new ResultViewModel();

            try
            {
                using (var bus = new EventBUS())
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