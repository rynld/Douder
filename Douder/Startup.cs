using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Douder.Models;
using System.Reflection;

[assembly: OwinStartup(typeof(Douder.Startup))]

namespace Douder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var builder = new ContainerBuilder();

            
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<DouderContext>().AsSelf().InstancePerLifetimeScope();            
            //builder.RegisterType<ApplicationUserManager>().InstancePerLifetimeScope();            

            //var container = builder.Build();

            //// Register the Autofac middleware FIRST. This also adds
            //// Autofac-injected middleware registered with the container.
            //app.UseAutofacMiddleware(container);

            ConfigureAuth(app);
        }
    }
}
