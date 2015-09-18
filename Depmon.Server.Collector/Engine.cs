using System;
using System.Threading;
using System.Threading.Tasks;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector
{
    public class Engine
    {
        private readonly MonitoringSection _config;
        private readonly CancellationTokenSource _cancellationSource;
        private Task[] _tasks;

        public Engine(MonitoringSection config)
        {
            _config = config;
            _cancellationSource = new CancellationTokenSource();
        }

        public void Start()
        {
            Console.WriteLine("Monitoring starting...");

            _tasks = new Task[_config.Mailboxes.Count];

            for (var i = 0; i < _config.Mailboxes.Count; i++)
            {
                Thread.Sleep(_config.Iteration.Delay);

                var mailbox = _config.Mailboxes[i];
                
                _tasks[i] = Task.Run(() => OnProcess(mailbox, _cancellationSource.Token), _cancellationSource.Token);
            }
        }

        private void OnProcess(Mailbox mailbox, CancellationToken cancellationToken)
        {
            var iterator = new Iterator(mailbox);
            Console.WriteLine("[{0}] monitoring started", mailbox.Name);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    iterator.Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[{1}] iteration failed: {0}", e.Message, mailbox.Name);
                }

                cancellationToken.WaitHandle.WaitOne(mailbox.Delay);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Monitoring breaking...");

            _cancellationSource.Cancel();
            Task.WaitAll(_tasks);
        }
    }
}