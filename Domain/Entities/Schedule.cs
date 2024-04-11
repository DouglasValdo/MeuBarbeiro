// ReSharper disable All

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Schedule", Schema = "Data")]
public partial class Schedule
{
    [Key]
    public Guid Id { get; set; }

    public Guid BarberId { get; set; }

    public Guid UserId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public Guid ScheduleTaskId { get; set; }

    public bool IsTerminated { get; set; }

    [ForeignKey("BarberId")]
    [InverseProperty("Schedules")]
    public virtual Barber Barber { get; set; } = null!;

    [ForeignKey("ScheduleTaskId")]
    [InverseProperty("Schedules")]
    public virtual ScheduleTask ScheduleTask { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Schedules")]
    public virtual User User { get; set; } = null!;
}
