using System;
using Core.Domain.Common;

namespace Core.Domain.Entities;

public class Product: EntityBase, IEntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int VAT { get; set; } = 18; // Value Added Tax
    public string SKU { get; set; }  // stock keeping unit
    public List<OrderItem> OrderItems { get; set; } // Relationships
}
