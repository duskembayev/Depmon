using System;
using System.Configuration;
using Autofac;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Collector.Impl.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class AutofacContainer
    {
        private static IContainer _container;

        public AutofacContainer()
        {
            if(_container != null) return;

            var builder = new ContainerBuilder();

            builder.Register(s => new UnitOfWork(getConnectionString("depmon"))).As<IUnitOfWork>().InstancePerLifetimeScope();
            
            builder.Register(s => new Engine(s.Resolve<IObjectFactory>())).As<IEngine>();

            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            builder.RegisterType<MailReciever>().As<IMailReciever>();
            builder.RegisterType<CsvParse>().As<ICsvParse>();
            
            builder.Register(s => new FactsSave(s.Resolve<IRepository<Report>>(), s.Resolve<IRepository<Fact>>())).As<IFactsSave>();

            builder.Register(s => new FactRepository(s.Resolve<IUnitOfWork>())).As<IRepository<Fact>>();
            builder.Register(s => new ReportRepository(s.Resolve<IUnitOfWork>())).As<IRepository<Report>>();
            
            builder.Register(s => new AutofacObjectFactory(_container)).As<IObjectFactory>();

            _container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return _container;
        }

        private string getConnectionString(string connStringName)
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
