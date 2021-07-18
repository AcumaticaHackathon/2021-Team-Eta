
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace JsonConfigurator
{
    public class Startup
    {
        public static void Configuration(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }

    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            GlobalConfiguration.Configure(Startup.Configuration);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}