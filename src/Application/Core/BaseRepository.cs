using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Core
{
    public interface BaseRepository<T>
    {
        Task<T> ReadAsync(Expression<Func<T, bool>> predicate);
        Task<T[]> ReadsAsync();
        Task<T[]> ReadsAsync(Expression<Func<T, bool>> predicate);
        Task<int> InsertAsync(T entity);
        Task<int> InsertsAsync(T[] entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdatesAsync(T[] entities);
        Task<int> DeleteAsync(T entity);
        Task<int> DeletesAsync(T[] entities);
    }
}
