using System;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();

        builder.Property(e => e.OrderNumber)
            .HasColumnName("order_number")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.OrderDate)
            .HasColumnName("order_date")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(e => e.ModifiedAt)
            .HasColumnName("modified_at");

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
