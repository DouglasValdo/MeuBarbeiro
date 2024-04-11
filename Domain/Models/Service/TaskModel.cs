namespace Domain.Models.Service;

public class TaskModel
{
    public string Name { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }
    public bool IsDeleted { get; set; }
    public long DurationInMinutes { get; set; }
}