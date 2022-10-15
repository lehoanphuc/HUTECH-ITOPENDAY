using JobFair.DomainModels;

namespace JobFair.ViewModels
{
    public class CompanyJobViewModel
    {
        public int IdCompany { get; set; }
        public int IdJobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string LinkJD { get; set; }
        public string CompanyLogo { get; set; }
        public bool IsApplied { get; set; }
        public string LinkInterview { get; set; }

        public CompanyJobViewModel() { }

        public CompanyJobViewModel(COMPANY_JOB item)
        {
            this.IdCompany = item.IdCompany;
            this.IdJobTitle = item.IdTitle;
            this.CompanyName = item.COMPANY.CompanyName;
            this.LinkJD = item.COMPANY.CompanyLink;
            this.CompanyLogo = item.COMPANY.IdCompany + ".png?v=" + item.COMPANY.ShowIndex;
            this.JobTitle = item.JOBTITLE.TitleName;
        }
    }
}
