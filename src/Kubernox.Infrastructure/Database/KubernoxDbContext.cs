using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Kubernox.Infrastructure.Database
{
    public class KubernoxDbContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<ClusterConfiguration> ClusterConfigurations { get; set; }

        public KubernoxDbContext(DbContextOptions<KubernoxDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KubernoxDbContext).Assembly);

        }
    }
}
