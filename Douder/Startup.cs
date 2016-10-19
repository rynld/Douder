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
using Microsoft.Owin.Security.OAuth;
using Douder.Autorization;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Douder.Startup))]

namespace Douder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext(()=> DependencyResolver.Current.GetService<ApplicationUserManager>());
            ConfigureAuth(app);            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }

  
}
