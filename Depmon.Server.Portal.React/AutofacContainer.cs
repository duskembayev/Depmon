using System;
using System.Configuration;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Depmon.Server.Database;
using Depmon.Server.Database.Impl;

namespace Depmon.Server.Portal.React
{
    public class AutofacContainer
    {
        private readonly IContainer _container;

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof (WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof (WebApiApplication).Assembly);

            builder.Register(s => new UnitOfWork(GetConnectionString("depmon"))).As<IUnitOfWork>().InstancePerLifetimeScope();

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