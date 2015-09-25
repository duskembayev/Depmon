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
        private IMailReciever _mailReciever;
        private IFactsSave _factsSave;

        public Engine(IMailReciever mailReciever, IFactsSave factsSave)
        {
            _cancellationSource = new CancellationTokenSource();
            _mailReciever = mailReciever;
            _factsSave = factsSave;
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
                    try
                    {
                        var data = _mailReciever.Load(mailbox);

                        if (data.Any())
                        {
                            _factsSave.Save(data);
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
