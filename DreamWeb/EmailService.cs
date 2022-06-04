using System.Net;
using System.Net.Mail;

namespace DreamWeb
{
    public interface IEmailService
    {
        public void SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService:  IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient("smtp-mail.outlook.com"))
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("dreamsnotebook.service@outlook.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.IsBodyHtml = false;
                mail.IsBodyHtml = true;
                mail.Body = message;
                
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(
                    _configuration["MailService:Email"],
                    _configuration["MailService:Password"]);
                client.EnableSsl = true;
                client.Send(mail);
            }
        }
    }
}
