using PetProjectC_NeuroWeb.Modules.HashModule.Ports;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.ConvertersModule;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.DataTransferObject;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.DTO;

namespace PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core.Core
{
    public static class UserAuthorizationService
    {
        public static async Task<TokenDTO> UserAuthorizationAsync(HttpContext context, IHashService hashService)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new UserJSONConverter());

            if (context.Request.HasJsonContentType())
            {

                var userDTO = await context.Request.ReadFromJsonAsync<UserDTO>(options);

                if (userDTO != null)
                {
                    using (var db = new DataBase())
                    {
                        var users = db.Users.ToList();
                        var checkingUser = users.FirstOrDefault(u => u.Login == userDTO.login);

                        if (checkingUser != null)
                        {
                            var checkingHashPassword = hashService.GenerateHashedPassword(userDTO.password, Encoding.UTF8.GetBytes(checkingUser.Salt));

                            if (checkingUser.HashedPassword == checkingHashPassword.ToString())
                            {
                                var accessTokenClaims = new List<Claim> { new Claim("userId", checkingUser.Id.ToString()) };

                                var accessToken = TokenOperations.GetAccessToken(accessTokenClaims);
                                var refreshToken = TokenOperations.GetRefreshToken();

                                return new TokenDTO(accessToken, refreshToken);

                                //JWTTokenGenerator.GenerateToken(userDTO,);

                            }
                        }
                    }
                }
            }
            return null;
        }





        private static byte[] GenerateSalt()
        {

            const int SALTLENGTH = 64;
            byte[] salt = new byte[SALTLENGTH];

            var cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetBytes(salt);



            return salt;
        }

    }



}
