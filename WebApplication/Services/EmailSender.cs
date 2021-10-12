using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("НаМаникюр.рф", "mail@xn--80aayihed0a9j.xn--p1ai"));
            message.To.Add(new MailboxAddress("Пользователь", email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mail.hosting.reg.ru", 465, true);
                // https://www.reg.ru/web-tools/punycode
                await client.AuthenticateAsync("mail@xn--80aayihed0a9j.xn--p1ai", "c29mh3F_");
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
