using System;
using System.Configuration;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deployment Health Monitor [Version 0.1]");
            Console.WriteLine("(c) Sergey Rubtsov, Dastan Uskembayev, 2015.");
            Console.WriteLine("\nPress 'X' for break...\n\n");

            var config = (MonitoringSection)ConfigurationManager.GetSection("monitoring");

            var engine = new Engine(config);
            engine.Start();

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