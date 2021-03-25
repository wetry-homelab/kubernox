using Application.Entities;
using Infrastructure.Persistence.Repositories;
using Kubernox.Tests.Fixtures;
using Kubernox.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kubernox.Tests.IntegrationTests.Repositories
{
    public class ClusterRepositoryTests : IClassFixture<ClusterNodeFixture>
    {
        private readonly ClusterNodeFixture fixture;

        public ClusterRepositoryTests(ClusterNodeFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ReadAllClusters()
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var nodes = await repository.ReadsAsync();

            Assert.Equal(4, nodes.Count());
        }

        [Theory]
        [InlineData("k3s-01")]
        [InlineData("k3s-03")]
        public async Task ReadCluster_ByName(string name)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var node = await repository.ReadAsync(c => c.Name == name);

            Assert.NotNull(node);
        }

        [Theory]
        [InlineData("testId01")]
        [InlineData("testId04")]
        public async Task ReadCluster_ById(string id)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var node = await repository.ReadAsync(c => c.Id == id);

            Assert.NotNull(node);
        }

        [Theory]
        [InlineData("k3s-20", "testId20", 2, 2)]
        [InlineData("k3s-21", "testId21", 2, 2)]
        public async Task InsertCluster_WithNameIdNodeAndCpu(string name, string id, int node, int cpu)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var inserted = await repository.InsertAsync(new Cluster()
            {
                Name = name,
                Id = id,
                Node = node,
                Cpu = cpu
            });

            Assert.Equal(1, inserted);
        }

        [Theory]
        [InlineData("testId01", 4)]
        [InlineData("testId04", 5)]
        public async Task UpdateCluster_ById_UpdateNode(string id, int node)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var cluster = await repository.ReadAsync(c => c.Id == id);
            Assert.NotNull(cluster);

            cluster.Node = node;
            var updated = await repository.UpdateAsync(cluster);

            Assert.Equal(1, updated);
        }

        [Theory]
        [InlineData("testId01")]
        [InlineData("testId04")]
        public async Task DeleteCluster_ById(string id)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var mock = new Mock<ILogger<ClusterRepository>>();
            ILogger<ClusterRepository> logger = mock.Object;

            var repository = new ClusterRepository(logger, dbContext);
            var cluster = await repository.ReadAsync(c => c.Id == id);
            Assert.NotNull(cluster);

            cluster.DeleteAt = DateTime.UtcNow;
            var deleted = await repository.DeleteAsync(cluster);

            Assert.Equal(1, deleted);
        }
    }
}
