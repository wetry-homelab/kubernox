using Application.Core;
using Application.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository : BaseRepository<User>
    {
        Task<User> ReadAsync(string userId);
    }
}
