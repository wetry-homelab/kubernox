using Kubernox.Infrastructure.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ReadAllUserAsync();
        Task<User> ReadUserAsync(User query);
        Task<int> UpdateUserAsync(User entity);
    }
}
