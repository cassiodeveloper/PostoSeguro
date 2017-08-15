using Swashbuckle.Application;
using System;
using System.Web.Http;

namespace PostoSeguro.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Posto Seguro API");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi(c => { });
        }

        protected static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\Swagger.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
