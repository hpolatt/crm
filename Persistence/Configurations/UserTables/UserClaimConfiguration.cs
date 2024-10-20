using System;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.UserTables;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        // Primary key
        builder.HasKey(uc => uc.Id);

            // Maps to the AspNetUserClaims table
        builder.ToTable("AspNetUserClaims");
    }
}
