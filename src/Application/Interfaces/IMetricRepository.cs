using Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMetricRepository
    {
        Task<Metric[]> ReadsMetrics(Expression<Func<Metric, bool>> predicate);
        Task<int> InsertMetricsAsync(Metric[] metrics);
        Task InsertMetricsWithStrategyAsync(Metric[] metrics);
    }
}
