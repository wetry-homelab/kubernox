using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using ProxmoxVEAPI.Client;
using ProxmoxVEAPI.Client.Contracts.Request;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class IdentityBusiness : IIdentityBusiness
    {
        public async Task<object> AuthenticateAsync(string username, string password)
        {
            UserClient userClient = new UserClient();

            var authResult = await userClient.AuthenticateUser(new AuthenticateContractRequest()
            {
                Username = username,
                Password = password
            });


            return null;
        }

        private string GenerateJwtToken(int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
