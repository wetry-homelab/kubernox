using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Humanizer.Localisation;

using Kubernox.Application.Interfaces;
using Kubernox.Shared.Contracts.Request;
using Kubernox.Shared.Contracts.Response;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kubernox.Application.Features.Identity.Commands
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse?>
    {
        private readonly IProxmoxClient proxmoxClient;
        private readonly IConfiguration configuration;

        public SignInCommandHandler(IProxmoxClient proxmoxClient, IConfiguration configuration)
        {
            this.proxmoxClient = proxmoxClient;
            this.configuration = configuration;
        }

        public async Task<SignInResponse?> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var isAuthenticatedUser = await proxmoxClient.AuthenticateUserAsync(request.SignInRequest.Username, request.SignInRequest.Password);
            if (isAuthenticatedUser)
            {
                return new SignInResponse()
                {
                    AccessToken = GenerateToken(request.SignInRequest.Username)
                };
            }
            return null;
        }


        private string GenerateToken(string username)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Identity:SecurityKey"]));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var jwt = new JwtSecurityToken(
                            issuer: configuration["Identity:Issuer"],
                            audience: configuration["Identity:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddDays(60),
                            signingCredentials: new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
