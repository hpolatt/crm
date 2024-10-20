using Core.Domain.Common;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class Role: IdentityRole<Guid>
{
    public virtual ICollection<UserRoles> UserRoles { get; set; }  
    public List<Permission> Permissions { get; set; } = new List<Permission>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
