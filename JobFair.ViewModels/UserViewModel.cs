using JobFair.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string Username { get; set; }

        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        public string Phone { get; set; }

        public int IdRole { get; set; }
        public string RoleName { get; set; }
        public int? IdCompany { get; set; }
        public string CompanyName { get; set; }
        public string Class { get; set; }

        public UserViewModel() { }

        public UserViewModel(USER item)
        {
            Id = item.IdUser;
            IdRole = item.IdRole;

            IdCompany = item.IdCompany;
            if (item.IdCompany.HasValue)
            {
                CompanyName = item.COMPANY.CompanyName;
            }

            Fullname = item.Fullname;
            RoleName = item.ROLE.RoleName;
            Phone = item.Phone ?? string.Empty;
            Email = item.Email ?? string.Empty;
            Username = item.Username;
            Class = item.ClassName ?? string.Empty;
        }
    }
}
