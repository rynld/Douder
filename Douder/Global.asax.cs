using Autofac;
using Autofac.Integration;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using Douder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Douder
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            //Register all Controllers with AutoFac so it will inject ProductEntities by calling
            //parameterized constructors
            
            builder.RegisterWebApiFilterProvider(config);


            //Initialize ProductEntities per request
            //builder.RegisterType<DouderContext>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();

            builder.RegisterType<DouderContext>().AsSelf().Instance‌​PerRequest();
            builder.RegisterType<UserStore<ApplicationUser>>().AsImplemented‌​Interfaces().Instanc‌​ePerRequest();
            builder.Register<IdentityFactoryOptions<ApplicationUserManag‌​er>>(c => new IdentityFactoryOptions<ApplicationUserManager>() {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionPr‌​ovider("Douder") });
            //builder.RegisterType<ApplicationUserManager>().AsSelf().Inst‌​ancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            



            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}
