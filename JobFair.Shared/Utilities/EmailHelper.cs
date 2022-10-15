using JobFair.Shared.MasterData;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace JobFair.Shared.Utilities
{
    public class EmailHelper
    {
        public static string EmailUser;
        public static string EmailPassword;
        public static string EmailSMTP = "smtp.gmail.com";
        public static string EmailSMTPPort = "587";
        public static string bodyTemplate;
        public static string buttonTemplate;

        public static void GetEmailAccount()
        {
            EmailUser = WebSettingData.EmailUser;
            EmailPassword = WebSettingData.EmailPassword;
        }

        public static void AutoGetSetting()
        {
            bodyTemplate = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Assets/template/email_template.vinh"));
        }

        public static void Send(string toEmail, string subject, string body, int failTimes = 0)
        {
            AutoGetSetting();
            GetEmailAccount();
            if (String.IsNullOrEmpty(EmailUser) || String.IsNullOrEmpty(EmailPassword) || String.IsNullOrEmpty(EmailSMTP) || String.IsNullOrEmpty(EmailSMTPPort)) throw new Exception("Kỹ thuật viên chưa truyền dữ liệu email");

            var smtp = new SmtpClient
            {
                Host = EmailSMTP,
                Port = int.Parse(EmailSMTPPort),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailUser, EmailPassword)
            };

            // add from,to mailaddresses
            MailAddress from = new MailAddress(EmailUser, "HUTECH IT OPEN DAY");
            MailAddress to = new MailAddress(toEmail);
            MailMessage myMail = new MailMessage(from, to);

            //// cc mail
            //MailAddress cc1 = new MailAddress("vinhphamdhsp@live.com");
            //MailAddress cc2 = new MailAddress("dt.phet@hutech.edu.vn");
            //MailAddress cc3 = new MailAddress("nt.dang@hutech.edu.vn");
            //myMail.CC.Add(cc1);
            //myMail.CC.Add(cc2);
            //myMail.CC.Add(cc3);

            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = Encoding.UTF8;
            // set body-message and encoding
            myMail.Body = bodyTemplate.Replace("{0}", body);
            myMail.BodyEncoding = Encoding.UTF8;

            // text or html
            myMail.IsBodyHtml = true;

            // Send mail, send fail thì xử lý lỗi
            smtp.Send(myMail);
        }
    }
}
