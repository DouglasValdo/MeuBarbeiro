namespace Domain.Services.Entities;

public class TaskModel
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Available { get; set; }
    public bool? IsDeleted { get; set; }
    public long? DurationInMinutes { get; set; }
}