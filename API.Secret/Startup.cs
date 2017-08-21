using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartup(typeof(API.Secret.Startup))]

namespace API.Secret
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureSwashbuckle(httpConfig);
            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        private void ConfigureSwashbuckle(HttpConfiguration config)
        {
            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "Identity server secret training API"))
                .EnableSwaggerUi();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>()
                .First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}