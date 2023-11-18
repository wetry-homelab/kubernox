using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public NodeRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public Task<List<Node>> GetNodesAsync()
        {
            return kubernoxDbContext.Nodes.ToListAsync();
        }

        public async Task<int> AddNodeAsync(Node entity)
        {
            await kubernoxDbContext.Nodes.AddAsync(entity);
            return await kubernoxDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateNodeAsync(Node entity)
        {
            kubernoxDbContext.Entry(entity).State = EntityState.Modified;
            return await kubernoxDbContext.SaveChangesAsync();
        }
    }
}
