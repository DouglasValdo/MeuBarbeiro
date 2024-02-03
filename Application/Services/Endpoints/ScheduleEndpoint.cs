using ApplicationStructure.Extensions;
using ApplicationStructure.Services.Endpoints;
using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
// ReSharper disable All

namespace ApplicationStructure.Services.Endpoints;

public class ScheduleEndpoint : EndpointsBase, IScheduleService
{
    private const string ENDPOINT = "Schedule/";
    
    public ScheduleEndpoint(HttpClient client) : base(client)
    {
    }

    public async Task<OperationOutcome<Schedule?>?> GetUserScheduleAsync(Guid userId)
    {
        var responseMessage = await Client.GetAsync($"{ENDPOINT}GetUserSchedule/{userId}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<Schedule?>>();
        
        return result;
    }

    public async Task<OperationOutcome?> DeleteScheduleAsync(Guid scheduleId)
    {
        var responseMessage = await Client.DeleteAsync($"{ENDPOINT}DeleteSchedule/{scheduleId}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome>();
        
        return result;
    }

    public async Task<OperationOutcome<IList<Schedule?>>?> GetUserTerminatedSchedulesAsync(Guid userId)
    {
        var responseMessage = await Client.GetAsync($"{ENDPOINT}GetAllUserTerminatedSchedules/{userId}");
        
        var result    = await responseMessage.Content.GetFromJsonAsync<OperationOutcome<IList<Schedule?>>>();
        
        return result;
    }
}