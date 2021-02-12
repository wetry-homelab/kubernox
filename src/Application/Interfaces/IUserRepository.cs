using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> ReadAsync(string userId);
        Task<int> InsertUserAsync(User entity);
    }
}
