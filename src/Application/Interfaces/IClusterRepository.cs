using Application.Core;
using Application.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClusterRepository : BaseRepository<Cluster>
    {
        Task<int> GetMaxOrder();
    }
}
