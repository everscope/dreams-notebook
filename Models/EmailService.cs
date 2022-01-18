using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace DreamWeb.Models
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService:  IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.Subject = subject;
            emailMessage.From.Add(new MailboxAddress("Dreams", "dreams.online.notebook@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("Visitor", email));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("dreams.online.notebook@gmail.com", "dreams.online.notebookdreams.online.notebook");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

        }
    }
}
