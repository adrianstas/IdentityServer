using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Tracing;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(API.Secret.Startup))]

namespace API.Secret
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            SystemDiagnosticsTraceWriter traceWriter = httpConfig.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = TraceLevel.Debug;

            ConfigureSwashbuckle(httpConfig);
            ConfigureWebApi(httpConfig);

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Constants.IssuerUri,
                RequiredScopes = new[] { "secret" }
            });

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        private void ConfigureSwashbuckle(HttpConfiguration config)
        {
            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
                .EnableSwaggerUi();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}