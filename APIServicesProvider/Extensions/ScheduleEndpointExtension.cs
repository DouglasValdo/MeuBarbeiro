using ApplicationStructure.DBContext;
using ApplicationStructure.Repositories;
using ApplicationStructure.Services.Operations;
using Domain.Entities;
using Domain.Models.Service;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ServicesProvider.Extensions;

public static class ScheduleEndpointExtension
{
    public static IEndpointRouteBuilder
        MapScheduleEndpoint(this WebApplication app, MeuBarbeiroDbContext dbContext)
    {
        IRepository<Schedule> scheduleRepository = new ScheduleRepository(dbContext);

        var scheduleEndPointOperations = new ScheduleServiceOperations(scheduleRepository);

        var scheduleGroupedEndpoint = app.MapGroup("/api/Schedule").WithTags("Schedule");
        
        scheduleGroupedEndpoint.MapPost("/AddSchedule", ([FromBody] ScheduleModel model)
            => scheduleEndPointOperations.Add(model));
        
        scheduleGroupedEndpoint.MapPut("/UpdateSchedule/{scheduleId:guid}", 
            (Guid scheduleId, [FromBody] ScheduleModel model)
                => scheduleEndPointOperations.Update(scheduleId, model));
        
        scheduleGroupedEndpoint.MapDelete("/DeleteSchedule/{scheduleId:guid}", (Guid scheduleId)
            => scheduleEndPointOperations.Delete(scheduleId));

        scheduleGroupedEndpoint.MapGet("/GetUserSchedules/{userId:guid}", (Guid userId)
            => scheduleEndPointOperations.GetUserSchedule(userId));

        scheduleGroupedEndpoint.MapGet("/GetAllUserTerminatedSchedules/{userId:guid}", (Guid userId)
            => scheduleEndPointOperations.GetAllUserTerminatedSchedules(userId));
       
        return app;
    }
}