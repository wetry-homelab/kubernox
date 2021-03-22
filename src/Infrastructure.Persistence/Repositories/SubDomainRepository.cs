using Application.Interfaces;
using Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SubDomainRepository : ISubDomainRepository
    {
        public Task<int> DeleteAsync(SubDomain entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(SubDomain[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(SubDomain entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertsAsync(SubDomain[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<SubDomain> ReadAsync(Expression<Func<SubDomain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<SubDomain[]> ReadsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SubDomain[]> ReadsAsync(Expression<Func<SubDomain, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(SubDomain entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(SubDomain[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
