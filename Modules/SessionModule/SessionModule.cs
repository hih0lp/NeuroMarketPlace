using PetProjectC_NeuroWeb.Modules.HashModule.Ports;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.ConvertersModule;
using System.Text.Json;
using PetProjectC_NeuroWeb.Modules.SessionModule.Converter;

namespace PetProjectC_NeuroWeb.Modules.SessionModule
{
    public class SessionModule : IModuleRegister
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("sessionmodule/getproducts",
                (HttpContext context) =>
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.Converters.Add(new ParserJSONConverter());

                    return context.Response.WriteAsJsonAsync(options);
                });

            return endpoints;
        }
    }
}
