using System;
using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public class Role: EntityBase, IEntityBase
{
    public string Name { get; set; }  // Example: "Admin", "User", "Manager"
    public List<Permission> Permissions { get; set; } = new List<Permission>();
}
