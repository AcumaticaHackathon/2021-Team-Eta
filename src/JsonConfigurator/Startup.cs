#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using Module = Autofac.Module;

namespace ClassLibrary1
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