namespace JobFair.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; }
        public string Repassword { get; set; }
        public string Token { get; set; }
        public string UserToken { get; set; }
        public string CurrentPassword { get; set; }
    }
}
