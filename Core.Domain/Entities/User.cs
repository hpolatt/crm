using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public Guid? CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public virtual ICollection<UserRoles> UserRoles { get; set; }    

}
