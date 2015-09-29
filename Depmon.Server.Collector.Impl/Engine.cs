using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector.Impl
{
    public class Engine : IEngine
    {
        private readonly CancellationTokenSource _cancellationSource;
        private Task[] _tasks;
        private IObjectFactory _objectFactory;

        public Engine(IObjectFactory objectFactory)
        {
            _cancellationSource = new CancellationTokenSource();
            _objectFactory = objectFactory;
        }

        public void Start(MonitoringSection config)
        {
            Console.WriteLine("Monitoring starting...");

            _tasks = new Task[config.Mailboxes.Count];

            for (var i = 0; i < config.Mailboxes.Count; i++)
            {
                Thread.Sleep(config.Iteration.Delay);

                var mailbox = config.Mailboxes[i];

                _tasks[i] = Task.Run(() => OnProcess(mailbox, _cancellationSource.Token), _cancellationSource.Token);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Monitoring breaking...");

            _cancellationSource.Cancel();
            Task.WaitAll(_tasks);
        }

        private void OnProcess(Mailbox mailbox, CancellationToken cancellationToken)
        {
            Console.WriteLine("[{0}] monitoring started", mailbox.Name);

            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _objectFactory.CreateScope())
                {
                    IMailReciever mailReciever = scope.Resolve<IMailReciever>();
                    IReportRegistry reportRegistry = scope.Resolve<IReportRegistry>();
                    ICsvReader csvReader = scope.Resolve<ICsvReader>();
                    IList<Stream> data = null;

                    try
                    {
                        data = mailReciever.Load(mailbox);

                        foreach (var stream in data)
                        {
                            var dtos = csvReader.Parse(stream);
                            if (!dtos.Any()) continue;
                            reportRegistry.Save(dtos);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[{1}] iteration failed: {0}", e.Message, mailbox.Name);
                    }
                    finally
                    {
                        Dispose(data);
                    }
                }
                cancellationToken.WaitHandle.WaitOne(mailbox.Delay);
            }
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
    }
}
