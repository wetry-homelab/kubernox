using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;

namespace Kubernox.Tests.Fixtures
{
    public class DatacenterNodeFixture : IDisposable
    {
        public void AddSeed(ServiceDbContext serviceDbContext)
        {
            serviceDbContext.DatacenterNode.Add(new DatacenterNode()
            {
                Name = "node01"
            });
            serviceDbContext.DatacenterNode.Add(new DatacenterNode()
            {
                Name = "node02"
            });
            serviceDbContext.DatacenterNode.Add(new DatacenterNode()
            {
                Name = "node03"
            });
            serviceDbContext.DatacenterNode.Add(new DatacenterNode()
            {
                Name = "node04"
            });

            serviceDbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
