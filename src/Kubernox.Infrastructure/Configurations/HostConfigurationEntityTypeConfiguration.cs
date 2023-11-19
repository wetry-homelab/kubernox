using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class HostConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<HostConfiguration>
    {
        public void Configure(EntityTypeBuilder<HostConfiguration> builder)
        {
            builder.ToTable("HostConfiguration");
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Name)
                        .IsRequired()
                        .HasMaxLength(128);

            builder.Property(p => p.Ip)
                        .IsRequired()
                        .HasMaxLength(128);

            builder.Property(p => p.ApiToken)
                        .IsRequired()
                        .HasMaxLength(250);
        }
    }
}
