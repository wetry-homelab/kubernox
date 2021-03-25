using Application.Core;
using Application.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISshKeyRepository : BaseRepository<SshKey>
    {
        Task<SshKey> ReadAsync(int id);
    }
}
