using log4net;
using log4net.Config;
using processOrderApi.Helpers;
using processOrderApi.Repositories;
using processOrderApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace processOrderApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configura log4net
            XmlConfigurator.Configure();

            // Configuración de HttpClient (Singleton)
            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

            // Registro de dependencias
            var container = new UnityContainer();
            container.RegisterType<IPedidoService, PedidoService>();
            container.RegisterType<IPedidoRepository, PedidoRepository>();
            container.RegisterType<ILogRepository, LogRepository>();
            container.RegisterType<IExternalValidationService, ExternalValidationService>(
                new InjectionConstructor(httpClient));
            container.RegisterType<DatabaseHelper>();

            // Registrar ILogger
            var log = LogManager.GetLogger(typeof(WebApiApplication));
            container.RegisterInstance<Interfaces.ILogger>(
                new Logging.Log4NetLogger(log));

            // Configuración de Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
