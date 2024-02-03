using ApplicationStructure.Services.Endpoints;
using Domain.Common.Service;
using Domain.Interfaces.Services;

namespace ApplicationStructure.Services;

public class ApplicationServiceEndpoints(HttpClient client) : IApplicationService
{
    public IScheduleService ScheduleService { get; } = new ScheduleEndpoint(client);
    public ITaskService TaskService { get; } = new TaskEndpoint(client);
    public IUserService UserService { get; } = new UserEndpoint(client);
}