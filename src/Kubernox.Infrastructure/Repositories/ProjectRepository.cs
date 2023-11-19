using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public ProjectRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public async Task<int> AddProjectAsync(Project entity)
        {
            await kubernoxDbContext.AddAsync(entity);
            return await kubernoxDbContext.SaveChangesAsync();
        }

        public Task<List<Project>> GetProjectsAsync()
        {
            return kubernoxDbContext.Projects.ToListAsync();
        }

    }
}
