namespace PetProjectC_NeuroWeb.Modules.OrderModule
{
    public class OrderModule : IModuleRegister
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
