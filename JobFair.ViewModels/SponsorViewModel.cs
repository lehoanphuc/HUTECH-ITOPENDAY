using System.Collections.Generic;

namespace JobFair.ViewModels
{
    public class SponsorViewModel
    {
        public string Title { get; set; }
        public List<string> CompanyName { get; set; }
        public List<CompanyViewModel> Company { get; set; }
    }
}
