using Kubernox.Domain.Entities;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface INodeRepository
    {
        Task<List<Node>> GetNodesAsync();
        Task<int> AddNodeAsync(Node entity);
        Task<int> UpdateNodeAsync(Node entity);
    }
}
