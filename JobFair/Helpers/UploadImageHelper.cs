using JobFair.Shared.MasterData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace JobFair.Helpers
{
    public class UploadImageHelper
    {
        public static void Upload(HttpPostedFileBase file, string fileName, string filePath, List<string> types)
        {
            // Create folder if not exist
            string path = GetServerPath() + "\\" + filePath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Set path save file
            string filename = fileName;
            path = Path.Combine(path, filename);

            // Validate file
            bool checkType = false;
            types.ForEach(type =>
            {
                if (file.FileName.EndsWith(type))
                {
                    checkType = true;
                    return;
                }
            });
            if (!checkType)
            {
                throw new Exception("Loại file không hợp lệ, vui lòng chỉ upload file hình ảnh");
            }

            if (file.ContentLength > WebSettingData.UploadFileSize * 1024 * 1024)
            {
                throw new Exception($"Vượt quá dung lượng tối đa được upload cho mỗi file hình là {WebSettingData.UploadFileSize} MB");
            }

            // Save file
            file.SaveAs(path);
        }

        public static string GetServerPath()
        {
            string root = HostingEnvironment.MapPath("~/");
            return root;
        }

        public static void Remove(string fileName, string filePath)
        {
            string serverPath = GetServerPath();
            string path = serverPath + "\\" + filePath + "\\" + fileName;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}