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
            ISettingsReader settingsReader = container.Resolve<ISettingsReader>();
            IEngine engine = container.Resolve<IEngine>();

            var config = settingsReader.Read();
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
