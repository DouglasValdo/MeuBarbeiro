// ReSharper disable All
namespace Domain.Entities;

public class Schedule
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string? Notes { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid TaskId { get; set; }

    public bool? IsTerminated { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}