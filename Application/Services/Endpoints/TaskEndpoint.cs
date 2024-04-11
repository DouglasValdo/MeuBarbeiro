using Domain.Common.Service;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace ApplicationStructure.Services.Endpoints;

public class TaskEndpoint(HttpClient client) : EndpointsBase(client), ITaskService
{
    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeByScheduleIdAsync(Guid scheduleId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationOutcome<ScheduleTask?>?> GetTaskTypeAsync()
    {
        throw new NotImplementedException();
    }
}