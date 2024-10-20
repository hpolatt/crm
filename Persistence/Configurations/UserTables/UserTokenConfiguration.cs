using System;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.UserTables;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserToken> b)
    {
        b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        // Limit the size of the composite key columns due to common DB restrictions
        b.Property(t => t.LoginProvider).HasMaxLength(450);
        b.Property(t => t.Name).HasMaxLength(450);

        // Maps to the AspNetUserTokens table
        b.ToTable("AspNetUserTokens");
    }
}
