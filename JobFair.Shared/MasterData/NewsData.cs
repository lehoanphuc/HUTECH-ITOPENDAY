using JobFair.ViewModels;
using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class NewsData
    {
        public static List<NewsViewModel> GetList()
        {
            var list = new List<NewsViewModel>();

            // add news
            list.Add(new NewsViewModel
            {
                Title = "“Sàn giao dịch” việc làm HUTECH IT Open Day 2021 chính thức khai mạc với hơn 3000 vị trí tuyển dụng",
                Url = "https://www.hutech.edu.vn/homepage/tin-tuc/tin-hutech/14591393-san-giao-dich-viec-lam-hutech-it-open-day-2021-chinh-thuc-khai-mac-voi-hon-3000-vi-tri-tuyen-dung",
                Img = "https://file1.hutech.edu.vn/file/editor/homepage1/IMG_8169.jpg"
            });

            list.Add(new NewsViewModel
            {
                Title = "32 doanh nghiệp công nghệ tuyển dụng sinh viên tại ngày hội HUTECH IT Open Day 2021",
                Url = "https://dantri.com.vn/giao-duc-huong-nghiep/32-doanh-nghiep-cong-nghe-tuyen-dung-sinh-vien-tai-hutech-it-open-day-2021-20210426164400276.htm",
                Img = "https://icdn.dantri.com.vn/thumb_w/640/2021/04/26/2404202132-doanhgnghiepcongnghetuyendungsinhvien-cntthutec-hdocx-1619430152397.jpeg"
            });

            list.Add(new NewsViewModel
            {
                Title = "Muốn biết ngành Công nghệ thông tin “hot” thế nào, hãy nhìn hơn 2000 đầu việc tại HUTECH IT Open Day 2021",
                Url = "https://kenh14.vn/muon-biet-nganh-cong-nghe-thong-tin-hot-the-nao-hay-nhin-hon-2000-dau-viec-tai-hutech-it-open-day-2021-20210426131437818.chn",
                Img = "https://kenh14cdn.com/thumb_w/800/pr/2021/1619415125396-60-0-953-1428-crop-1619415131661-63755039677922.jpg"
            });

            list.Add(new NewsViewModel
            {
                Title = "‘HUTECH IT Open Day’ - ngày hội giải ‘cơn khát’ nguồn nhân lực Công nghệ thông tin",
                Url = "https://thanhnien.vn/hutech-it-open-day-ngay-hoi-giai-con-khat-nguon-nhan-luc-cong-nghe-thong-tin-post840383.html",
                Img = "https://image.thanhnien.vn/w2048/Uploaded/2022/puqgfdmzs.co/2019_04_09/hutech/hutech1_ecyl.jpg"
            });

            list.Add(new NewsViewModel
            {
                Title = "Hutech mở sàn giao dịch hơn 1.500 việc làm tại ngày hội việc làm HUTECH IT OPEN Day",
                Url = "https://zingnews.vn/hutech-mo-san-giao-dich-hon-1500-viec-lam-post1097289.html",
                Img = "https://znews-photo.zadn.vn/w860/Uploaded/wyhktpu/2020_06_18/image001_3.jpg"
            });

            list.Add(new NewsViewModel
            {
                Title = "Dàn chuyên gia công nghệ hàng đầu “vạch kế hoạch” cho sinh viên Công nghệ thông tin HUTECH",
                Url = "https://kenh14.vn/dan-chuyen-gia-cong-nghe-hang-dau-vach-ke-hoach-cho-sinh-vien-cong-nghe-thong-tin-hutech-20220120120920899.chn",
                Img = "https://channel.mediacdn.vn/2022/1/20/photo-2-16426545717141826614630.jpg"
            });

            return list;
        }
    }
}
