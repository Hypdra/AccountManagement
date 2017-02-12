using Owin;
using System.Web.Http;
using System;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using AccountManagement.WebApi.Providers;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using AccountManagement.Login;

namespace AccountManagement.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            
            WebApiConfig.Register(config);
            BusConfig.ConfigureBus();
            app.UseCors(CorsOptions.AllowAll);

            var container = BuildContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            ConfigureOAuth(app, container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<LoginModule>();
            builder.RegisterType<AccountManagementAuthorizationProvider>()
                   .As<IOAuthAuthorizationServerProvider>()
                   .PropertiesAutowired()
                   .SingleInstance();
            return builder.Build();
        }

        private void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            var oauthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>()
            };

            app.UseOAuthAuthorizationServer(oauthServerOptions);
            app.UseOAuthBearerTokens(oauthServerOptions);
        }
    }
}