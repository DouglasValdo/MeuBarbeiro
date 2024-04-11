using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ScheduleTask", Schema = "Data")]
public partial class ScheduleTask
{
    [Key]
    public Guid Id { get; set; }

    public Guid BarberId { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsDeleted { get; set; }

    public long DurationInMinutes { get; set; }

    [ForeignKey("BarberId")]
    [InverseProperty("ScheduleTasks")]
    public virtual Barber Barber { get; set; } = null!;

    [InverseProperty("ScheduleTask")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
