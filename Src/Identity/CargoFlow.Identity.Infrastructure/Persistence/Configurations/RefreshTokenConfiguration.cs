using CargoFlow.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Token)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.Revoked).IsRequired();
        builder.HasIndex(x => x.Token);
        builder.Property<Guid>("user_id");
    }
}
