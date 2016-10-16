using Autofac;
using Autofac.Integration;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using Douder.Models;
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            //Initialize ProductEntities per request
            builder.RegisterType<DouderContext>().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            BundleConfig.RegisterBundles(BundleTable.Bundles);



        }




    }
}
