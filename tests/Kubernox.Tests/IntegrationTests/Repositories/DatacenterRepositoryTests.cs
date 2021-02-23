using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Kubernox.Tests.Fixtures;
using Kubernox.Tests.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kubernox.Tests.IntegrationTests.Repositories
{
    public class DatacenterRepositoryTests : IClassFixture<DatacenterNodeFixture>
    {
        public readonly DatacenterNodeFixture fixture;

        public DatacenterRepositoryTests(DatacenterNodeFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ReadAllNodes()
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);
            var repository = new DatacenterRepository(dbContext);
            var nodes = await repository.ReadsAsync();

            Assert.Equal(4, nodes.Count());
        }

        [Theory]
        [InlineData("node01")]
        [InlineData("node03")]
        public async Task ReadNode_ByName(string name)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);
            var repository = new DatacenterRepository(dbContext);
            var node = await repository.ReadAsync(c => c.Name == name);

            Assert.NotNull(node);
        }

        [Theory]
        [InlineData("node200", 4)]
        [InlineData("node201", 8)]
        public async Task InsertNode_WithNameAndCpu(string name, int cpu)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);
            var repository = new DatacenterRepository(dbContext);
            var inserted = await repository.InsertAsync(new DatacenterNode()
            {
                Name = name,
                Core = cpu
            });

            Assert.Equal(1, inserted);
        }

        [Theory]
        [InlineData("node01", 1024)]
        [InlineData("node03", 8192)]
        public async Task UpdateNode_ByName_UpdateRam(string name, int ram)
        {
            var dbContext = DbContextTestHelper.GetDbContext();
            fixture.AddSeed(dbContext);

            var repository = new DatacenterRepository(dbContext);
            var node = await repository.ReadAsync(c => c.Name == name);
            Assert.NotNull(node);

            node.RamTotal = ram;
            var updated = await repository.UpdateAsync(node);

            Assert.Equal(1, updated);
        }
    }
}
