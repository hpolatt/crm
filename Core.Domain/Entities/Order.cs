using Core.Domain.Common;

namespace Core.Domain.Entities;

public class Order: EntityBase, IEntityBase
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get => OrderItems.Sum(item => item.Price * item.Quantity); }
}
