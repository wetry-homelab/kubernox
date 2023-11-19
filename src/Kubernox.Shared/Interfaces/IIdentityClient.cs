using Kubernox.Shared.Contracts.Request;
using Kubernox.Shared.Contracts.Response;

namespace Kubernox.Shared.Interfaces
{
    public interface IIdentityClient
    {
        Task<SignInResponse> SignInAsync(SignInRequest request);
        Task<string> TestAsync();
    }
}
