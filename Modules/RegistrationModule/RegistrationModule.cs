using PetProjectC_NeuroWeb.Modules.RegistrationModule.Ports;
using PetProjectC_NeuroWeb.Modules.RegistrationModule.Services;

namespace PetProjectC_NeuroWeb.Modules.RegistrationModule
{
    public class RegistrationModule
    {
        public IServiceCollection RegisterService(IServiceCollection services)
        {

            return services;
        }
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}
