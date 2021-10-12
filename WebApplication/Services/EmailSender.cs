using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration configuration;

        public EmailSender(IConfiguration config)
        {
            this.configuration = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var server = configuration.GetValue<string>("MailConfig:Server");
            var port = configuration.GetValue<int>("MailConfig:Port");
            var address = configuration.GetValue<string>("MailConfig:Address");
            var password = configuration.GetValue<string>("MailConfig:Password");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Наманикюр.рф", address));
            message.To.Add(new MailboxAddress("Пользователь", email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port, true);
                // https://www.reg.ru/web-tools/punycode
                await client.AuthenticateAsync(address, password);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
