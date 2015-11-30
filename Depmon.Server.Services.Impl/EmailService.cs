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

                client.Connect(smtpConfig.Server, smtpConfig.Port, smtpConfig.Ssl);
                client.Authenticate(smtpConfig.Username, smtpConfig.Password);

                var message = new MimeMessage();

                var mailboxAddresses = new List<MailboxAddress>();

                foreach (var recipient in data.Recipient)
                {
                    mailboxAddresses.Add(new MailboxAddress(recipient,recipient));
                }


                message.From.Add(new MailboxAddress(data.Sender, data.Sender));
                message.To.AddRange(mailboxAddresses);
                message.Subject = data.Subject;
                message.Body = new TextPart("plain") {Text = data.Body};

                client.Send(message);
            }
        }
    }
}
