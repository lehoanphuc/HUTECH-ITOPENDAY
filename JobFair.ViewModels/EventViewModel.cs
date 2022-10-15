using JobFair.DomainModels;
using System;
using System.Collections.Generic;

namespace JobFair.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public DateTime? DateTimeType { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public bool AllowReg { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int CountStudent { get; set; }

        public EventViewModel() {  }

        public EventViewModel(EVENT item)
        {
            this.Id = item.IdEvent;
            this.Name = item.EventName;
            this.Place = item.EventLocation;
            this.Description = item.EventDescription;
            this.AllowReg = item.IsShow == true;
            this.DateTimeType = item.EventTime;
            this.CountStudent = item.STUDENT_EVENT.Count;
            this.Time = ConvertDateToString();
        }

        public string ConvertDateToString()
        {
            if (!this.DateTimeType.HasValue)
            {
                return string.Empty;
            }

            List<string> dayOfWeekString = new List<string>()
            {
                "Chủ nhật",
                "Thứ hai",
                "Thứ ba",
                "Thứ tư",
                "Thứ năm",
                "Thứ sáu",
                "Thứ bảy"
            };
            var dateString = dayOfWeekString[(int)DateTimeType.Value.DayOfWeek] + ", " + DateTimeType.Value.ToString("dd/MM/yyyy lúc HH:mm");
            return dateString;
        }

        public void SetDBModel(EVENT model)
        {
            model.EventName = Name;
            model.EventTime = DateTimeType;
            model.EventLocation = Place;
            model.EventDescription = Description;
            model.IsShow = AllowReg;
        }
    }
}
