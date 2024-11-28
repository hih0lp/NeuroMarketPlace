using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.ConvertersModule;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.DataTransferObject;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using PetProjectC_NeuroWeb.Modules.RegistrationModule.Services;
using PetProjectC_NeuroWeb.Modules.RegistrationModule.Ports;
using PetProjectC_NeuroWeb.Modules.HashModule.Ports;

namespace PetProjectC_NeuroWeb.Modules.UserModule.UserModule.Endpoint
{

    public partial class UserEndpoint
    {
        public async Task RegisterUser(HttpContext context, IHashService hashService)
        {
            await UserRegistrationService.UserRegistrationAsync(context, hashService);   
        }
    }
}

