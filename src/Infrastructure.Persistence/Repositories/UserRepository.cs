using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
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
        public Task<int> InsertUserAsync(User entity)
        {
            serviceDbContext.User.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }
        public Task<int> UpdateUserAsync(User entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<User> ReadAsync(string userId)
        {
            return serviceDbContext.User.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
