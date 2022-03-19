using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IIdentityBusiness
    {
        Task<AuthenticationResponse> AuthenticateUserAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> UpdatePasswordAsync(PasswordUpdateRequest request);
    }
}
