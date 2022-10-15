using JobFair.DomainModels;

namespace JobFair.ViewModels
{
    public class CandidateViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string StudentCode { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Class { get; set; }
        public string JobTitle { get; set; }

        public CandidateViewModel() { }

        public CandidateViewModel(USER item)
        {
            Id = item.IdUser;
            StudentCode = item.Username;
            Fullname = item.Fullname;
            Phone = item.Phone;
            Email = item.Email;
            Class = item.ClassName;
        }
    }
}
