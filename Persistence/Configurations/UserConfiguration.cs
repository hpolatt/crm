using System;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(e => e.CustomerId)
            .HasColumnName("customer_id");

        builder.Property(e => e.ModifiedAt)
            .HasColumnName("modified_at");

        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        // CustomerId
        builder.HasOne(u => u.Customer)
               .WithMany(c => c.Users)
               .HasForeignKey(u => u.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(e => e.UserRoles)  
                .WithOne(e => e.User)  
                .HasForeignKey(ur => ur.UserId)  
                .IsRequired();                 
    }
}
