using CargoFlow.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoFlow.Identity.Infrastructure.Persistence.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .ValueGeneratedNever();

        builder.Property(x => x.Name)
        .HasMaxLength(100)
        .IsRequired();

        builder.HasIndex(x => x.Name)
        .IsUnique();

        ConfigurePermissions(builder);
    }

    private static void ConfigurePermissions(
    EntityTypeBuilder<Role> builder)
    {
        builder
        .HasMany<Permission>(x=>x.Permissions)
        .WithMany()
        .UsingEntity<Dictionary<string, object>>(
        "role_permissions",
        right => right
        .HasOne<Permission>()
        .WithMany()
        .HasForeignKey("permission_id"),
        left => left
        .HasOne<Role>()
        .WithMany()
        .HasForeignKey("role_id"),
        join =>
        {
            join.ToTable("role_permissions");

            join.HasKey(
                "role_id",
                "permission_id");
        });

        builder.Navigation(nameof(Role.Permissions))
        .UsePropertyAccessMode(
        PropertyAccessMode.Field);
    }
}
