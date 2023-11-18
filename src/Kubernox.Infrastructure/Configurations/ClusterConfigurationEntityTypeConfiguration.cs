using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class ClusterConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ClusterConfiguration>
    {
        public void Configure(EntityTypeBuilder<ClusterConfiguration> builder)
        {
            builder.ToTable("ClusterConfiguration");
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
