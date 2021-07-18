#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

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