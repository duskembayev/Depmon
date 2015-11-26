using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Depmon.Server.Collector.Configuration;
using MailKit.Net.Pop3;
using MimeKit;


namespace Depmon.Server.Collector.Impl
{
    public class MailReciever : IMailReciever
    {
        public IList<Stream> Load(Mailbox mailbox)
        {
            IList<Stream> result = new List<Stream>();

            using (var client = new Pop3Client())
            {
                client.Connect(mailbox.Server, mailbox.Port, mailbox.Ssl);
                client.Authenticate(mailbox.Username, mailbox.Password);

                var msgCount = client.Count;
                Console.WriteLine("[{1}] {0} new messages found", msgCount, mailbox.Username);

                for (var li = 0; li < msgCount; li++)
                {
                    try
                    {
                        var letter = client.GetMessage(li);
                        
                        var parts = letter.BodyParts;
                        foreach (var part in parts)
                        {
                            if (part.IsAttachment)
                            {
                                var stream = new MemoryStream();
                                ((MimePart)part).ContentObject.WriteTo(stream);
                                stream.Position = 0;
                                result.Add(stream);
                            }
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
                client.Disconnect(true);
            }

            return result;
        }

        private bool CertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
