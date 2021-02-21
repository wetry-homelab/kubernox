using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public UserRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }
        public Task<int> InsertAsync(User entity)
        {
            serviceDbContext.User.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }
        public Task<int> UpdateAsync(User entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<User> ReadAsync(string userId)
        {
            return serviceDbContext.User.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public Task<User> ReadAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> ReadsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User[]> ReadsAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertsAsync(User[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(User[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(User[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
