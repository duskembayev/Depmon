using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Collector.Impl.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class AutofacContainer
    {
        private IContainer _container;

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //builder.RegisterType<Engine>().As<IEngine>();
            builder.Register(s => new Engine(s.Resolve<IMailReciever>(), s.Resolve<IFactsSave>())).As<IEngine>();

            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            builder.RegisterType<MailReciever>().As<IMailReciever>();
            
            //builder.RegisterType<FactsSave>().As<IFactsSave>();
            builder.Register(s => new FactsSave()).As<IFactsSave>();

            builder.RegisterType<FactRepository>().As<IRepository<Fact>>();
            builder.RegisterType<ReportRepository>().As<IRepository<Report>>();

            _container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return _container;
        }
    }
}
