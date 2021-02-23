using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kubernox.Tests.Helpers
{
    public static class DbContextTestHelper
    {
        public static DbContextOptions<ServiceDbContext> CreateDbContextOptions()
        {
            return new DbContextOptionsBuilder<ServiceDbContext>()
                //Database with same name gets reused, so let's isolate the tests from each other...
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
                .Options;
        }

        public static ServiceDbContext GetDbContext()
        {
            return new ServiceDbContext(CreateDbContextOptions());
        }
    }
}
