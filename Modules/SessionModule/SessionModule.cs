using PetProjectC_NeuroWeb.Modules.HashModule.Ports;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.ConvertersModule;
using System.Text.Json;
using PetProjectC_NeuroWeb.Modules.SessionModule.DTO;
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

                    //database request

                    //crewate dt0o

                    using (var db = new DataBase())
                    {
                        string[] namesList = null!;
                        string[] reviewsList = null;
                        string[] idsList = null!;

                        var products = db.Products.ToList();

                        foreach (var product in products)
                        {
                            namesList.Append(product.Name);
                            reviewsList.Append(product.Comment.Review);
                            idsList.Append(product.Id);
                        }

                        var productsData = new PicturesDTO(namesList, reviewsList, idsList);

                        context.Response.WriteAsJsonAsync<PicturesDTO>(productsData, options);
                    }
                });

            return endpoints;
        }
    }
}
