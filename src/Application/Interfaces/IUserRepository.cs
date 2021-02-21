using Application.Core;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository : BaseRepository<User>
    {
        Task<User> ReadAsync(string userId);
    }
}
