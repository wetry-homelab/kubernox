using Application.Entities;
using Infrastructure.Persistence.Contexts;
using System;

namespace Kubernox.Tests.Fixtures
{
    public class ClusterNodeFixture : IDisposable
    {
        public void AddSeed(ServiceDbContext serviceDbContext)
        {
            serviceDbContext.Cluster.Add(new Cluster()
            {
                Name = "k3s-01",
                Id = "testId01",
                OrderId = 3000,
                State = "Ready"
            });

            serviceDbContext.Cluster.Add(new Cluster()
            {
                Name = "k3s-02",
                Id = "testId02",
                OrderId = 3010,
                State = "Ready",
                DeleteAt = DateTime.UtcNow.AddDays(-1)
            });

            serviceDbContext.Cluster.Add(new Cluster()
            {
                Name = "k3s-03",
                Id = "testId03",
                OrderId = 3020,
                State = "Ready"
            });

            serviceDbContext.Cluster.Add(new Cluster()
            {
                Name = "k3s-04",
                Id = "testId04",
                OrderId = 3030,
                State = "Waiting",
                Cpu = 2,
                Node = 4
            });

            serviceDbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
