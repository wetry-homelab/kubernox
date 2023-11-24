using Kubernox.Domain.Entities;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface ITemplateRepository
    {
        Task<List<Template>> GetTemplatesAsync();
        Task<int> AddTemplateAsync(Template entity);
        Task<int> UpdateTemplateAsync(Template entity);
    }
}
