using Domain.Common.Service;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITaskService
{
    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeByScheduleIdAsync(Guid scheduleId);
    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeAsync();
}