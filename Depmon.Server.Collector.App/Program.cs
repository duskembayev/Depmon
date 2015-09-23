using System;
using Autofac;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Collector.Impl;

namespace Depmon.Server.Collector.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deployment Health Monitor [Version 0.1]");
            Console.WriteLine("(c) Sergey Rubtsov, Dastan Uskembayev, 2015.");
            Console.WriteLine("\nPress 'X' for break...\n\n");

            IContainer container = new AutofacContainer().GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                IConfigReader cr = scope.Resolve<IConfigReader>();
                var config = cr.Read();

                IEngine engine = scope.Resolve<IEngine>();
                engine.Start(config);

                var finish = false;
                do
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.X)
                    {
                        finish = true;
                    }
                } while (!finish);

                engine.Stop();
            }
        }
    }
}
