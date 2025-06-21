using System.ComponentModel.DataAnnotations.Schema;

namespace SCGR.Domain.Abstractions.Types;

public abstract class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
}
