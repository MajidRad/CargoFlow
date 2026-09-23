using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoFlow.Identity.Infrastructure.Persistence;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_log");
        builder.HasKey(a=> a.Id);
        builder.Property(a=>a.Id).ValueGeneratedNever();
        builder.Property(a => a.UserId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(a=>a.Action).HasColumnName("action").IsRequired();
        builder.Property(a=>a.TimeStamp).HasColumnName("timestamp").IsRequired();  
    }
}