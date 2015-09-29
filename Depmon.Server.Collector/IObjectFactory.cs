using Autofac;

namespace Depmon.Server.Collector
{
	public interface IObjectFactory
    {
        ILifetimeScope CreateScope();
    }
}
