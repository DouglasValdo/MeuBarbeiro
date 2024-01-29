namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public int PhoneNumber { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}