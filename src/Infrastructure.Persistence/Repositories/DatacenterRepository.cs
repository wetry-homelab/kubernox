using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DatacenterRepository : IDatacenterRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public DatacenterRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Task<DatacenterNode> ReadAsync(Expression<Func<DatacenterNode, bool>> predicate)
        {
            return serviceDbContext.DatacenterNode.FirstOrDefaultAsync(predicate);
        }

        public Task<DatacenterNode[]> ReadsAsync()
        {
            return serviceDbContext.DatacenterNode.ToArrayAsync();
        }

        public Task<int> InsertAsync(DatacenterNode entity)
        {
            serviceDbContext.DatacenterNode.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(DatacenterNode entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<DatacenterNode[]> ReadsAsync(Expression<Func<DatacenterNode, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertsAsync(DatacenterNode[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(DatacenterNode[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(DatacenterNode entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(DatacenterNode[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
