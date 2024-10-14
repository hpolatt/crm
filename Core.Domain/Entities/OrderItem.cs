using Core.Domain.Common;

namespace Core.Domain.Entities;

public class OrderItem: EntityBase, IEntityBase
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } 
    public Guid ProductId { get; set; } 
    public Product Product { get; set; }
    public decimal Price { get => Product.Price;}
    public int Quantity { get; set; } = 1;
    public int Discount { get; set; } = 0; // Discount in percentage
    // Gross Total Price 100 $ - 10% = 90 $
    public decimal GrossTotalPrice { get => Price * Quantity - (Price * Quantity * Discount / 100); }
    // Net Total Price 100 $ - 10% = 90 $ + 10% VAT = 99 $
    // VAT = 10% of Gross Total Price
    public decimal NetTotalPrice { get => GrossTotalPrice + (GrossTotalPrice * (1.0m * Product.VAT/100)); }

}
