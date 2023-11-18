using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class NodeEntityTypeConfiguration : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.ToTable("Node");
            builder.HasKey(c => c.Id);
        }
    }
}
