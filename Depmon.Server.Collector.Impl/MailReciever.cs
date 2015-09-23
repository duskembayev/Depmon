using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Depmon.Server.Collector.Configuration;
using OpenPop.Pop3;

namespace Depmon.Server.Collector.Impl
{
    public class MailReciever : IMailReciever
    {
        public IList<Stream> Load(Mailbox mailbox)
        {
            IList<Stream> result = new List<Stream>();

            using (var client = new Pop3Client())
            {
                client.Connect(mailbox.Server, mailbox.Port, mailbox.Ssl, mailbox.Timeout, mailbox.Timeout, CertificateValidator);
                client.Authenticate(mailbox.Username, mailbox.Password);

                var lCount = client.GetMessageCount();
                Console.WriteLine("[{1}] {0} new messages found", lCount, mailbox.Username);

                for (var li = 1; li <= lCount; li++)
                {
                    try
                    {
                        var letter = client.GetMessage(li);
                        var attachments = letter.FindAllAttachments();
                        foreach (var attachment in attachments)
                        {
                            var stream = new MemoryStream();
                            attachment.Save(stream);
                            stream.Position = 0;

                            result.Add(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Message recieve error: {0}", ex.Message);
                    }
                    finally
                    {
                        client.DeleteMessage(li);
                    }
                }
            }

            return result;
        }

        private bool CertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
