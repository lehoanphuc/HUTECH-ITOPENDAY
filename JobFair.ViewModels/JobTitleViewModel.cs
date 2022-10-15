using JobFair.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    public class JobTitleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public JobTitleViewModel() { }

        public JobTitleViewModel(JOBTITLE item)
        {
            this.Id = item.IdTitle;
            this.Title = item.TitleName;
        }
    }
}
