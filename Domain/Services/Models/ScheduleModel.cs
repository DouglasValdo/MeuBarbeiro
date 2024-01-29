namespace Domain.Services.Entities;

public class ScheduleModel
{
    public bool? IsDeleted { get; set; }
    public Guid UserId { get; set; }
    public string? Notes { get; set; }
    public Guid TaskId { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool? IsTerminated { get; set; }
}