using Autofac;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Collector.Impl.Configuration;
using Depmon.Server.Database;

namespace Depmon.Server.Collector.Impl
{
    public class AutofacContainer
    {
        private static IContainer _container;

        static AutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<Engine>().As<IEngine>();
            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            builder.RegisterType<MailReciever>().As<IMailReciever>();
            builder.RegisterType<FactsSave>().As<IFactsSave>();

            _container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return _container;
        }
    }
}
