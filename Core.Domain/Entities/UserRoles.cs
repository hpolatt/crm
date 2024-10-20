using System;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class UserRoles: IdentityUserRole<Guid>  
{
    public User User { get; set; }
    public Role Role { get; set; }
}
