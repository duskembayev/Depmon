using System.Collections.Generic;
using Depmon.Server.Collector.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace Depmon.Server.Services.Impl
{
    public class EmailService:IEmailService
    {
        private readonly Settings _config;

        public EmailService(ISettingsReader settingsReader)
        {
            _config = settingsReader.Read();
        }


        public void SendNotification(IData data)
        {
            using (var client = new SmtpClient())
            {
                var smtpConfig = _config.Notification.Sender;

                client.Connect(smtpConfig.Server, smtpConfig.Port);
                client.Authenticate(smtpConfig.Username, smtpConfig.Password);

                var message = new MimeMessage();

                var mailboxAddresses = new List<MailboxAddress>();

                mailboxAddresses.Add(new MailboxAddress(data.Recipient, data.Recipient));
                

                message.From.Add(new MailboxAddress(smtpConfig.Username, smtpConfig.Username));
                message.To.AddRange(mailboxAddresses);
                message.Subject = data.Subject;
                message.Body = new TextPart("html") {Text = data.Body};

                client.Send(message);
            }
        }
    }
}
