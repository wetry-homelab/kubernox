using Kubernox.Domain.Entities;

namespace Kubernox.Infrastructure.Interfaces
{
    public interface IHostConfigurationRepository
    {
        Task<List<HostConfiguration>> GetHostsAsync();
        Task<int> AddHostConfigurationAsync(HostConfiguration hostConfiguration);   
        Task<int> UpdateHostConfigurationAsync(HostConfiguration hostConfiguration);   
    }
}
