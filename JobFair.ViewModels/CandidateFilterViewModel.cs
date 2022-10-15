using System.Collections.Generic;

namespace JobFair.ViewModels
{
    public class CandidateFilterViewModel
    {
        public List<CompanyViewModel> Companies { get; set; } = new List<CompanyViewModel>();
        public List<JobTitleViewModel> JobTitles { get; set; } = new List<JobTitleViewModel>();
    }
}
