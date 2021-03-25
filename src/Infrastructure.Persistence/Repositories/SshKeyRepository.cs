using Application.Interfaces;
using Application.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SshKeyRepository : ISshKeyRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public SshKeyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Task<SshKey[]> ReadsAsync()
        {
            return serviceDbContext.SshKey.Where(s => !s.DeleteAt.HasValue).ToArrayAsync();
        }

        public Task<SshKey> ReadAsync(int id)
        {
            return serviceDbContext.SshKey.FirstOrDefaultAsync(s => !s.DeleteAt.HasValue && s.Id == id);
        }

        public Task<int> InsertAsync(SshKey entity)
        {
            serviceDbContext.SshKey.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(SshKey sshKey)
        {
            serviceDbContext.Entry(sshKey).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<SshKey> ReadAsync(Expression<Func<SshKey, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<SshKey[]> ReadsAsync(Expression<Func<SshKey, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertsAsync(SshKey[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(SshKey[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(SshKey entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(SshKey[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
