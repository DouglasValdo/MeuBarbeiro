// ReSharper disable All
namespace Domain.Entities;

public class Schedule
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string? Notes { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid ScheduleTaskId { get; set; }

    public bool? IsTerminated { get; set; }

    public virtual ScheduleTask ScheduleTask { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}