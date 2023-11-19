using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class SshKeyEntityTypeConfiguration : IEntityTypeConfiguration<SshKey>
    {
        public void Configure(EntityTypeBuilder<SshKey> builder)
        {
            builder.ToTable("SshKey");
            builder.HasKey(c => c.Id);
        }
    }
}
