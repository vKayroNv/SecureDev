using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardStorageService.Core.Utils
{
    internal static class TokenUtils
    {
        private const string SecretKey = "IWhdOiJxO2Y4cl9USiEjYW9bQndGYl94WSZxUDpxUw";

        public static (DateTime created, DateTime closed, string token) GenerateJwtToken(int id)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] { new(ClaimTypes.NameIdentifier, id.ToString()) })
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return (securityToken.ValidFrom, securityToken.ValidTo, jwtSecurityTokenHandler.WriteToken(securityToken));
        }
    }
}
