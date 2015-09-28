using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                _objectFactory.CreateScope();

                IMailReciever mailReciever = _objectFactory.Create<IMailReciever>();
                IFactsSave factsSave = _objectFactory.Create<IFactsSave>();
                ICsvParse csvParse = _objectFactory.Create<ICsvParse>();

                try
                {
                    var data = mailReciever.Load(mailbox);

                    foreach (var stream in data)
                    {
                        var dtos = csvParse.Parse(stream);
                        if (!dtos.Any()) continue;
                        factsSave.Save(dtos);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[{1}] iteration failed: {0}", e.Message, mailbox.Name);
                }

                cancellationToken.WaitHandle.WaitOne(mailbox.Delay);
            }
        }
    }
}
