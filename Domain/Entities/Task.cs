namespace Domain.Entities;

public class Task
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public bool Available { get; set; }

    public bool? IsDeleted { get; set; }

    public long? DurationInMinutes { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}