using System.ComponentModel.DataAnnotations;

namespace JobFair.ViewModels
{
    /// <summary>
    /// Using in login page
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Student { get; set; }
    }
}
