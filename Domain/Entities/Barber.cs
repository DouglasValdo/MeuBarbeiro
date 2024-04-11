using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Barber", Schema = "Data")]
public partial class Barber
{
    [Key]
    public Guid Id { get; init; }

    public string Name { get; set; }

    public DateTime ScheduleStart { get; set; }

    public DateTime ScheduleEnd { get; set; }

    [Required]
    public bool? IsDeleted { get; set; }

    public byte? Status { get; set; }

    [InverseProperty("Barber")]
    public virtual ICollection<ScheduleTask> ScheduleTasks { get; init; } = new List<ScheduleTask>();

    [InverseProperty("Barber")]
    public virtual ICollection<Schedule> Schedules { get; init; } = new List<Schedule>();
}
