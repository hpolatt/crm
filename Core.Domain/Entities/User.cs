using System;
using Core.Domain.Common;

namespace Core.Domain.Entities;

public class User: EntityBase, IEntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    // Relationships
    public ICollection<Customer> Customers { get; set; }
}
