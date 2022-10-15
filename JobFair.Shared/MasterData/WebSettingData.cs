using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class WebSettingData
    {
        public static string Title = "HUTECH IT OPEN DAY 2022";
        public static string Description = "Với chuỗi sự kiện từ tháng 03/2021 đến tháng 04/2022, ngày hội sẽ đưa các bạn khám phá, gắn kết và tương tác với nhau bao gồm Hội thảo “Định hướng nghề IT & Kỹ năng chinh phục nhà tuyển dụng”, “Tuần triển lãm thực tập/việc làm” và “Ngày hội tuyển dụng, triển lãm công nghệ”";
        public static string Keyword = "hutech it, cntt hutech, khoa cntt hutech, it openday, ngay hoi cntt hutech, event it hutech, công nghệ phần mềm, phần mềm hutech, clb youthdev, youthdev hutech, youth dev, ngành cntt hutech, hutech đào tạo cntt, dev hutech, coder hutech, lập trình, học lập trình ở hutech, học it ở hutech, hutech youthdev, công nghệ thông tin, hutech it, cntt hutech, học cntt hutech, clb hutech, clb it hutech, câu lạc bộ it hutech, clb cntt hutech, hutech";
        public static string ThumbImg = "/Assets/img/banner1.jpg";

        // Config
        public static string LinkLogoCompany = "Assets/img/logo_company";
        public static string LinkCV = "Assets/upload/cv";
        public static string LinkScholarshipPoint = "Assets/upload/scholarship/point";
        public static string LinkScholarshipCV = "Assets/upload/scholarship/cv";

        // Captcha
        public static string CaptchaSiteID = "6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN";
        public static string CaptchaSecretKey = "6Le2hPsUAAAAAHKoQY_Q4Duw_aaMNbpAoSSjleJ4";

        // Facebook ngày hội
        public static string FacebookUrl = "https://www.facebook.com/HutechITOpenday";

        // Video youtube
        public static string VideoUrl = "https://www.youtube.com/embed/vGzpIvlINzQ?controls=0&showinfo=0&rel=0&autoplay=1&loop=1&mute=1&playlist=vGzpIvlINzQ";

        // Upload file
        // Limit size by megabyte
        public static int UploadFileSize = 5;
        public static List<string> UploadFileTypes = new List<string>() { ".png" };
        public static List<string> UploadFilePointTypes = new List<string>() { ".pdf", ".csv", ".xlsx", ".xls" };

        // Email
        public static string EmailUser = "hutechcheckin@gmail.com";
        public static string EmailPassword = "Vinh@2981995";

    }
}
