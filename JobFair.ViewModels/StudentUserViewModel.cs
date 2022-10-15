using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    public class StudentUserViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [MinLength(9, ErrorMessage = "Vui lòng nhập chính xác MSSV hoặc CMND của bạn")]
        [MaxLength(15, ErrorMessage = "Vui lòng nhập chính xác MSSV hoặc CMND của bạn")]
        public string Username { get; set; }

        public string Password { get; set; }
        public string Repassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Fullname { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
        public string Class { get; set; }

        public StudentUserViewModel() { }

        public StudentUserViewModel(NameValueCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                var value = form.Get(key);
                switch (key.ToLower())
                {
                    case "regusername":
                    case "username":
                        Username = value;
                        break;
                    case "regname":
                    case "fullname":
                        Fullname = value;
                        break;
                    case "regemail":
                    case "email":
                        Email = value;
                        break;
                    case "regphone":
                    case "phone":
                        Phone = value;
                        break;
                    case "regclass":
                    case "class":
                        Class = value;
                        break;
                    case "regpassword":
                    case "password":
                        Password = value;
                        break;
                    case "regrepassword":
                    case "repassword":
                        Repassword = value;
                        break;
                }
            }
        }
    }
}
