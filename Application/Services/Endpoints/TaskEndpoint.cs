using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace ApplicationStructure.Services.Endpoints;

public class TaskEndpoint : EndpointsBase, ITaskService
{
    public TaskEndpoint(HttpClient client) : base(client)
    {
    }

    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeByScheduleIdAsync(Guid scheduleId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeAsync()
    {
        throw new NotImplementedException();
    }
}