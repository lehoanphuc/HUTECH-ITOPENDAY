using JobFair.DomainModels;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace JobFair.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Video { get; set; }
        public string Description { get; set; }
        public string LinkJD { get; set; }
        public string MeetingLink { get; set; }

        public List<int> JobTitleIds { get; set; } = new List<int>();

        public CompanyViewModel() { }

        public CompanyViewModel(COMPANY item)
        {
            Id = item.IdCompany;
            Name = item.CompanyName;
            Logo = item.IdCompany.ToString() + ".png?v=" + item.ShowIndex;
            Description = item.CompanyDescription;
            Video = item.CompanyVideo;
            LinkJD = item.CompanyLink;
            MeetingLink = item.MeetingLink;

            if (item.COMPANY_JOB != null)
            {
                JobTitleIds = item.COMPANY_JOB.Where(x => x.IsDeleted != true).Select(x => x.IdTitle).ToList();
            }
        }

        public CompanyViewModel(NameValueCollection form) 
        {
            foreach (var key in form.AllKeys)
            {
                var value = form.Get(key);
                switch (key.ToLower())
                {
                    case "id":
                        Id = int.Parse(value);
                        break;
                    case "name":
                        Name = value;
                        break;
                    case "logo":
                        Logo = value;
                        break;
                    case "description":
                        Description = value;
                        break;
                    case "video":
                        Video = value;
                        break;
                    case "linkjd":
                        LinkJD = value;
                        break;
                    case "meetinglink":
                        MeetingLink = value;
                        break;
                    case "jobtitle":
                        var ids = value.Split(',');
                        foreach (var id in ids)
                        {
                            JobTitleIds.Add(int.Parse(id));
                        }
                        break;
                }
            }
        }
    }
}
