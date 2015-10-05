using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Depmon.Server.Portal
{
    public class MvcApplication : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureIoC();
        }

        private void ConfigureIoC()
        {
            var container = new AutofacContainer().GetContainer();

            // WebAPI
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}