using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobFair.Controllers
{
    [UserAuthencation(new int[] { (int)RoleKeys.ADMIN })]
    public class SettingController : Controller
    {
        [Route("admin/setting/sponsor")]
        public ActionResult Sponsor()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/setting/sponsor")]
        public ActionResult Sponsor(string value)
        {
            try
            {
                var check = JsonConvert.DeserializeObject<List<SponsorViewModel>>(value);
                using (var bus = new SettingBUS())
                {
                    bus.Save(SettingKeys.SETTING_SPONSOR.ToString(), value);
                }
                ViewBag.Success = "Lưu dữ liệu thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không đúng format";
            }
            return View();
        }
    }
}