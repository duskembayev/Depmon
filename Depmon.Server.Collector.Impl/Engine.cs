using System;
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

        public Engine()
        {
            _cancellationSource = new CancellationTokenSource();
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

            IContainer continer = new AutofacContainer().GetContainer();

            using (var scope = continer.BeginLifetimeScope())
            {
                IDataLoad dataLoad = scope.Resolve<IDataLoad>();
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var data = dataLoad.Load(mailbox);

                        if (data.Any())
                        {
                            IDataSave dataSave = scope.Resolve<IDataSave>();
                            dataSave.Save(data);
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
}
