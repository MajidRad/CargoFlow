using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoFlow.Identity.Infrastructure.Persistence;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(
            id => id.Value,
            value => new UserId(value)
            ).ValueGeneratedNever();

        builder.Property(u => u.Email);
        builder.Property(u => u.Email)
            .HasConversion(
             email => email.Value,
             value => Email.Create(value)
            )
            .HasColumnName("email")
            .IsRequired();

        builder.OwnsOne(u => u.FullName, fn =>
        {
            fn.Property(f => f.FirstName)
            .HasColumnName("first_name")
            .IsRequired();
            fn.Property(f => f.LastName)
            .HasColumnName("last_name")
            .IsRequired();
        });

        builder.OwnsOne(u => u.UserMetadata, md =>
        {
            md.Property(m => m.Language)
            .HasColumnName("language")
            .HasMaxLength(10);
            md.Property(m => m.TimeZone)
            .HasColumnName("time_zone")
            .HasMaxLength(50);
            md.Property(m=>m.Theme)
            .HasColumnName("theme")
            .HasMaxLength(20);
        });
        
        builder.Property(u => u.KeyCloakId)
            .HasColumnName("keycloak_id")
            .IsRequired(true);

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired(true);


        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
    }
}
