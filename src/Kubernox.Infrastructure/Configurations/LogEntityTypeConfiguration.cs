using Kubernox.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubernox.Infrastructure.Configurations
{
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log");
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Type)
                    .IsRequired()
                    .HasMaxLength(128);

            builder.Property(p => p.Data)
                    .IsRequired();
        }
    }
}
