using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Common;

public abstract class EntityBase : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}
