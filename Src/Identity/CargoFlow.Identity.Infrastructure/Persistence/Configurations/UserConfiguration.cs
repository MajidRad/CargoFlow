using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoFlow.Identity.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration
: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .ValueGeneratedNever();

        builder.Property(x => x.IsActive)
        .IsRequired();

        ConfigureName(builder);

        ConfigureEmail(builder);

        ConfigurePassword(builder);

        ConfigureRoles(builder);

        ConfigureRefreshTokens(builder);
    }

    private static void ConfigureName(
    EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

            name.Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();
        });
    }

    private static void ConfigureEmail(
    EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

            email.HasIndex(x => x.Value)
            .IsUnique();
        });
    }

    private static void ConfigurePassword(
    EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(x => x.PasswordHash, password =>
        {
            password.Property(x => x.Value)
            .HasColumnName("password_hash")
            .IsRequired();
        });
    }

    private static void ConfigureRoles(
    EntityTypeBuilder<User> builder)
    {
        builder
        .HasMany<Role>(x=>x.Roles)
        .WithMany()
        .UsingEntity<Dictionary<string, object>>(
        "user_roles",
        right => right
        .HasOne<Role>()
        .WithMany()
        .HasForeignKey("role_id"),
        left => left
        .HasOne<User>()
        .WithMany()
        .HasForeignKey("user_id"),
        join =>
        {
            join.ToTable("user_roles");

            join.HasKey(
            "user_id",
            "role_id");
        });

        builder.Navigation(nameof(User.Roles))
        .UsePropertyAccessMode(
        PropertyAccessMode.Field);
    }

    private static void ConfigureRefreshTokens(
    EntityTypeBuilder<User> builder)
    {
        builder.Metadata
        .FindNavigation(nameof(User.RefreshTokens))!
        .SetPropertyAccessMode(
        PropertyAccessMode.Field);

        builder.HasMany<RefreshToken>(x=>x.RefreshTokens)
        .WithOne()
        .HasForeignKey("user_id")
        .OnDelete(DeleteBehavior.Cascade);
    }
}