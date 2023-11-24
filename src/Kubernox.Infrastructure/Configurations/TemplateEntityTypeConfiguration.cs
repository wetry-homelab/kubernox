using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Template");
            builder.HasKey(c => c.Id);

            builder.Property(p => p.VmId)
                   .IsRequired();

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(128);
        }
    }
}
