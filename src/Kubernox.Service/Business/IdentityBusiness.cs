using Application.Interfaces;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IConfiguration configuration;
        private readonly ILogger<IdentityBusiness> logger;

        public IdentityBusiness(IConfiguration configuration, ILogger<IdentityBusiness> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        {
            UserClient userClient = new UserClient();
            logger.LogInformation($"Informations for login : {request.Username} - {request.Password}");

            var authResult = await userClient.AuthenticateUser(new AuthenticateContractRequest()
            {
                Username = request.Username,
                Password = request.Password
            });

            if (authResult)
            {
                var jwtToken = GenerateJwtToken(request.Username);

                return new AuthResponse()
                {
                    Success = true,
                    Token = jwtToken
                };
            }

            return null;
        }

        private string GenerateJwtToken(string accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
