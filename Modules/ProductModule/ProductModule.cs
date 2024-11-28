using PetProjectC_NeuroWeb.Modules.ProductModule.Core;
using Microsoft.AspNetCore.Authorization;

namespace PetProjectC_NeuroWeb.Modules.ProductModule
{
    public class ProductModule : IModuleRegister
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        { 
            return services;
        }
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("products/addproduct", 
            [Authorize]
            (HttpContext context) => ProductOperationsService.AddProductToCatalog(context));

            endpoints.MapPost("products/addproducttousercart",
                [Authorize]
            (string accessJWTToken, string productId) => ProductOperationsService.AddProductToUserCart(accessJWTToken, productId));


            return endpoints;
        }
    }
}
