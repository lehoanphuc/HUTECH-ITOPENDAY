using JobFair.ViewModels;
using System.Collections.Generic;

namespace JobFair.Shared.MasterData
{
    public class EventData
    {
        public static List<EventViewModel> GetList()
        {
            var list = new List<EventViewModel>();

            // add event
            list.Add(new EventViewModel
            {
                Name = "Triển lãm thông tin tuyển dụng Việc làm/Thực tập",
                Url = "/job",
                Time = "15/03/2022 - 15/04/2022",
                Place = "Website & Fanpage",
                AllowReg = true,
                Description = "Với sự tham gia của 3000+ ứng viên/sinh viên",
                Img = "/Assets/img/event/1.png"
            });

            list.Add(new EventViewModel
            {
                Name = "Định hướng nghề và Kỹ năng chinh phục nhà tuyển dụng",
                Url = "/event",
                Time = "28/03/2022 - 06/04/2022",
                Place = "HUTECH",
                AllowReg = true,
                Description = "Gồm 12 hội thảo với các diễn giả là lãnh đạo, chuyên gia do các công ty hàng đầu ngành, Hội tin học TP. HCM và liên minh công nghệ số Việt Nam",
                Img = "/Assets/img/event/2.png"
            });

            //list.Add(new EventViewModel
            //{
            //    Name = "Ngày hội Tuyển dụng và Triển lãm Công nghệ thông tin",
            //    Url = "",
            //    Time = "06h30 ngày 08/04/2022",
            //    Place = "HUTECH",
            //    AllowReg = false,
            //    Description = "Với sự tham gia của 2000+ ứng viên/sinh viên",
            //    Img = "/Assets/img/event/3.png"
            //});

            return list;
        }
    }
}
