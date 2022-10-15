using JobFair.ViewModels;
using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class VideoData
    {
        public static List<VideoViewModel> GetVideo()
        {
            var list = new List<VideoViewModel>();
            list.Add(new VideoViewModel
            {
                Name = "Hơn 3000 vị trí tuyển dụng, thực tập về CNTT tại HUTECH IT Open day 2021",
                Link = "https://www.youtube.com/embed/vGzpIvlINzQ"
            });

            list.Add(new VideoViewModel
            {
                Name = "Lạc giữa ngày hội việc làm của sinh viên IT HUTECH",
                Link = "https://www.youtube.com/embed/iLWsFOV_k2c"
            });

            list.Add(new VideoViewModel
            {
                Name = "Hơn 2000 vị trí tuyển dụng, thực tập về CNTT tại HUTECH IT Open day 2019",
                Link = "https://www.youtube.com/embed/yfnwIKggFyU"
            });

            list.Add(new VideoViewModel
            {
                Name = "HUTECH IT OPEN DAY 2018: Hàng ngàn cơ hội việc làm cho sinh viên CNTT",
                Link = "https://www.youtube.com/embed/xuIyemq_gqA"
            });

            list.Add(new VideoViewModel
            {
                Name = "Hơn 3000 vị trí tuyển dụng, thực tập về CNTT tại HUTECH IT Open day 2021",
                Link = "https://www.youtube.com/embed/vGzpIvlINzQ"
            });

            list.Add(new VideoViewModel
            {
                Name = "Hơn 3000 vị trí tuyển dụng, thực tập về CNTT tại HUTECH IT Open day 2021",
                Link = "https://www.youtube.com/embed/vGzpIvlINzQ"
            });
            return list;
        }
    }
}
