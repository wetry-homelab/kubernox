using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(p => p.Identifier)
                   .IsRequired();
        }
    }
}
