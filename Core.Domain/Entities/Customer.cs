using Core.Domain.Common;

namespace Core.Domain.Entities;

public class Customer: EntityBase, IEntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Company { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<User> Users { get; set; }
}