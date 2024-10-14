namespace Core.Domain.Common;

public interface IEntityBase
{
    Guid Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
}
