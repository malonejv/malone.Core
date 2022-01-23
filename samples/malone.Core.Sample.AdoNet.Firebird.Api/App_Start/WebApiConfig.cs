using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Routing;

namespace malone.Core.Sample.AdoNet.Firebird.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // we only need to change the default constraint resolver for services that want urls with versioning like: ~/v{version}/{controller}
            var constraintResolver = new DefaultInlineConstraintResolver() { ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) } };

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            //config.MapHttpAttributeRoutes();

            // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
            config.AddApiVersioning(options => options.ReportApiVersions = true);
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "v{apiVersion}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { apiVersion = new ApiVersionRouteConstraint() }
            );

            //Descomentar si no queremos devolver XML nunca
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Definimos el formato JSON por defecto
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //EnableCorsAttribute enableCorsAttribute = new EnableCorsAttribute("http://url-frontend", "*", "*");
            EnableCorsAttribute enableCorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCorsAttribute);

            //Importar de malone.Core.WebApi, configurar aqui para requerir https en toda la aplicacion
            //config.Filters.Add(RequireHttpsAttribute);

            SwaggerConfig.Register();
        }
    }
}
