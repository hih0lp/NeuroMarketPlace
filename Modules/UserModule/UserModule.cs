using Microsoft.AspNetCore.Builder;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core.Core;
using PetProjectC_NeuroWeb.Modules.HashModule.Ports;
using PetProjectC_NeuroWeb.Modules.RegistrationModule.Services;
namespace PetProjectC_NeuroWeb.Modules.User_Module
{
    public class UserModule : IModuleRegister
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("user/register",
                (HttpContext context, IHashService hashService) => UserRegistrationService.UserRegistrationAsync(context, hashService));

            endpoints.Map("user/authorization",
                (HttpContext context, IHashService hashService) => UserAuthorizationService.UserAuthorizationAsync(context, hashService));

            return endpoints;
        }
    }
}
