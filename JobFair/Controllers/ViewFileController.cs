using JobFair.Helpers;
using JobFair.Services.BUS;
using JobFair.Shared.Constants;
using JobFair.Shared.MasterData;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace JobFair.Controllers
{
    public class ViewFileController : Controller
    {
        [UserAuthencation(new int[] { (int)RoleKeys.STUDENT })]
        [Route("mycv")]
        public void MyCV()
        {
            int id = SessionHelper.GetUser().IdUser;
            var filePath = WebSettingData.LinkCV + "\\" + id + ".pdf";
            viewFilePath(filePath);
        }

        [UserAuthencation(new int[] { (int)RoleKeys.ADMIN })]
        [Route("admin/scholarshipcv/{id}")]
        public void ScholarshipCV(int id)
        {
            var filePath = WebSettingData.LinkScholarshipCV + "\\" + id + ".pdf";
            viewFilePath(filePath);
        }

        [UserAuthencation(new int[] { (int)RoleKeys.ADMIN })]
        [Route("admin/scholarshippoint/{id}")]
        public void ScholarshipPoint(int id)
        {
            foreach (var type in WebSettingData.UploadFilePointTypes)
            {
                var filePath = WebSettingData.LinkScholarshipPoint + "\\" + id + type;
                if (viewFilePath(filePath, false))
                {
                    return;
                }
            }
        }

        [UserAuthencation(new int[] { (int)RoleKeys.ADMIN, (int)RoleKeys.COMPANY })]
        [Route("admin/candidatecv/{id}")]
        public void CandidateCV(int id)
        {
            var userObj = SessionHelper.GetUser();

            // Check role and permisssion
            if (userObj.IdRole == (int)RoleKeys.COMPANY)
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

                if (!allowCV)
                {
                    Response.Write("Quản trị viên đã tạm khóa tính năng xem CV ứng viên, vui lòng trở lại sau, cám ơn.");
                    return;
                }

                // Check company
                var candidateBUS = new CandidateBUS();
                if (!candidateBUS.CheckCandidate(id, userObj.IdCompany ?? -1))
                {
                    Response.Write("Bạn không có quyền xem CV ứng viên này");
                    return;
                }
            }

            var filePath = WebSettingData.LinkCV + "\\" + id + ".pdf";
            viewFilePath(filePath);
        }

        private bool viewFilePath(string filePath, bool returnNotFoundMsg = true)
        {
            string root = Server.MapPath("~");
            filePath = root + "\\" + filePath;
            WebClient User = new WebClient();

            if (!System.IO.File.Exists(filePath))
            {
                if (!returnNotFoundMsg)
                {
                    return false;
                }

                Response.Write("Không tìm thấy file này");
                return false;
            }

            Byte[] FileBuffer = User.DownloadData(filePath);
            if (FileBuffer != null)
            {
                var fileName = System.IO.Path.GetFileName(filePath);
                var fileType = filePath.Split('.').Last();

                Response.ContentType = "application/" + fileType;
                Response.AddHeader("content-disposition", "attachment;filename=\"" + fileName + "\"");
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);

                return true;
            }

            return false;
        }
    }
}