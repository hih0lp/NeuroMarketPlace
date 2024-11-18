using Microsoft.IdentityModel.Tokens;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core.Core;
using PetProjectC_NeuroWeb.Modules.AuthorizationModule.DTO;
using PetProjectC_NeuroWeb.Modules.UserModule.UserModule.DataTransferObject;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PetProjectC_NeuroWeb.Modules.AuthorizationModule.Core
{
    public class TokenOperations
    {
        public static AuthOptions tokenOptions { get; set; } = null!;

        public static string GetAccessToken(List<Claim> claims)
        {
            var _claims = claims;

            var JWT = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: _claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(tokenOptions.GetSymmetricKey(), SecurityAlgorithms.HmacSha512)
                );

            return new JwtSecurityTokenHandler().WriteToken(JWT);
        }


        private static bool CheckAccessToken(string token)
        {
            var decodedToken = DecodeToken(token);
            return true;
        }

        public static string GetRefreshToken()
        {
            var rndmNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(rndmNumber);
            return Convert.ToHexString(rndmNumber);
        }

        public static Dictionary<string, string> DecodeToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        }

        public static async Task<TokenDTO> RefreshToken(string refreshToken, string accessToken)
        {
            var decodedAccessToken = DecodeToken(accessToken);
            //var decodedRefreshToken = DecodeToken(refreshToken);

            //var refreshTokenClaims = new List<Claim>();
            var accessTokenClaims = new List<Claim>();

            foreach (var claim in decodedAccessToken)
            {
                accessTokenClaims.Add(new Claim(claim.Key, claim.Value));
            }

            //foreach (var claim in decodedRefreshToken)
            //{
            //    refreshTokenClaims.Add(new Claim(claim.Key, claim.Value));
            //}

            var user = await UserTokenService.GetUserByRefreshTokenAsync(refreshToken);

            if (user != null)
            {
                using (var db = new DataBase())
                {
                    user.RefreshToken = GetRefreshToken();
                    user.AccessToken = GetAccessToken(accessTokenClaims);
                    db.SaveChanges();

                    return new TokenDTO(user.RefreshToken, user.AccessToken);
                }


            }
            return null;
        }
    }
}
