using Application.Core;
using Application.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMetricRepository : BaseRepository<Metric>
    {
        Task InsertMetricsWithStrategyAsync(Metric[] metrics);
    }
}
