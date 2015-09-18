using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;
using OpenPop.Pop3;

namespace Depmon.Server.Collector
{
    public class Iterator
    {
        private readonly Mailbox _mailbox;

        public Iterator(Mailbox mailbox)
        {
            _mailbox = mailbox;
        }

        public void Execute()
        {
            var data = Load();

            if (!data.Any()) return;

            using (var connection = ConnectionFactory.Instance.Create())
            using (var rRepo = new ReportRepository(connection))
            using (var fRepo = new FactRepository(connection))
            {
                foreach (var stream in data)
                {
                    var reportId = rRepo.Create();

                    Process(stream, reportId, fRepo);
                }
            }

            Dispose(data);
        }

        private void Process(Stream data, long reportId, FactRepository factRepository)
        {
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true,
            };
            csvConfig.RegisterClassMap<FactMap>();
            csvConfig.CultureInfo = CultureInfo.InvariantCulture;

            try
            {
                using (var reader = new StreamReader(data, Encoding.UTF8))
                using (var parser = new CsvReader(reader, csvConfig))
                {
                    var dtos = parser.GetRecords<Fact>().ToArray();
                    foreach (var fact in dtos)
                    {
                        fact.ReportId = reportId;
                    }

                    factRepository.InsertMany(dtos);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CSV parsing error: {0}", e.Message);
            }
        }

        private IList<Stream> Load()
        {
            IList<Stream> result = new List<Stream>();

            using (var client = new Pop3Client())
            {
                client.Connect(_mailbox.Server, _mailbox.Port, _mailbox.Ssl, _mailbox.Timeout, _mailbox.Timeout, CertificateValidator);
                client.Authenticate(_mailbox.Username, _mailbox.Password);

                var lCount = client.GetMessageCount();
                Console.WriteLine("[{1}] {0} new messages found", lCount, _mailbox.Name);

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

        private void Dispose(IList<Stream> data)
        {
            foreach (var stream in data)
            {
                stream.Close();
                stream.Dispose();
            }

            data.Clear();
        }

        private bool CertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}