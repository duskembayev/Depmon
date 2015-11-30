using System;
using System.Configuration;
using Autofac;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Collector.Impl;
using Depmon.Server.Collector.Impl.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Database.Impl;
using Depmon.Server.Domain;
using Depmon.Server.Services.Impl;

namespace Depmon.Server.Collector.App
{
    public class AutofacContainer
    {
        private static IContainer _container;

        public AutofacContainer()
        {
            if(_container != null) return;

            var builder = new ContainerBuilder();

            builder.Register(s => new UnitOfWork(GetConnectionString("depmon"))).As<IUnitOfWork>().InstancePerLifetimeScope();
            
            builder.RegisterType<Engine>().As<IEngine>();

            builder.RegisterType<SettingsReader>().As<ISettingsReader>();
            builder.RegisterType<MailReciever>().As<IMailReciever>();
            builder.RegisterType<CsvReader>().As<ICsvReader>();
            
            builder.RegisterType<ReportRegistry>().As<IReportRegistry>();

            builder.RegisterType<FactRepository>().As<IRepository<Fact>>();
            builder.RegisterType<ReportRepository>().As<IRepository<Report>>();

            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();


            builder.Register(s => new AutofacObjectFactory(_container)).As<IObjectFactory>();

            _container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return _container;
        }

        private string GetConnectionString(string connStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connStringName];
            if (connectionString == null)
            {
                throw new ApplicationException($"Connection string {connStringName} not found");
            }

            return connectionString.ConnectionString;
        }
    }
}
