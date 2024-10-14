using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Configurations;

public class PermissionValueComparer : ValueComparer<List<Permission>>
{
    public PermissionValueComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToList())
    {
    }
}