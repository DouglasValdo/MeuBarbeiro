using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ScheduleTask
{
    public Guid Id { get; set; }

    [MaxLength(500)]
    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public bool IsAvailable { get; set; }

    public bool? IsDeleted { get; set; }

    public long? DurationInMinutes { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}