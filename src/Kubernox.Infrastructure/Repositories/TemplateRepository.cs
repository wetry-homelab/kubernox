using Kubernox.Domain.Entities;
using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly KubernoxDbContext kubernoxDbContext;

        public TemplateRepository(KubernoxDbContext kubernoxDbContext)
        {
            this.kubernoxDbContext = kubernoxDbContext;
        }

        public Task<List<Template>> GetTemplatesAsync()
        {
            return kubernoxDbContext.Templates.ToListAsync();
        }

        public async Task<int> AddTemplateAsync(Template entity)
        {
            await kubernoxDbContext.AddAsync(entity);
            return await kubernoxDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateTemplateAsync(Template entity)
        {
            kubernoxDbContext.Entry(entity).State = EntityState.Modified;
            return await kubernoxDbContext.SaveChangesAsync();
        }
    }
}
