using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new )
    }
}
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
            md.Property("theme")
            .HasColumnName("theme")
            .HasMaxLength(20);
        });
        
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired(true);

        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
    }
}
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