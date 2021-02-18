using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityBusiness
    {
        Task<AuthResponse> AuthenticateAsync(AuthRequest request);
    }
}
