﻿using Application.DBContext;
using Application.Repositories;
using Application.Services.Operations;
using Domain.Entities;
using Domain.Repository;
using Domain.Services.Entities;
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
        
        scheduleGroupedEndpoint.MapPost("/AddSchedule/{model}", ([FromBody] ScheduleModel model)
            => scheduleEndPointOperations.Add(model));
        
        scheduleGroupedEndpoint.MapPut("/UpdateSchedule/{scheduleId:guid}/{model}", 
            (Guid scheduleId, [FromBody] ScheduleModel model)
                => scheduleEndPointOperations.Update(scheduleId, model));
        
        scheduleGroupedEndpoint.MapDelete("/DeleteSchedule/{scheduleId:guid}", (Guid scheduleId)
            => scheduleEndPointOperations.Delete(scheduleId));

        scheduleGroupedEndpoint.MapGet("/GetUserSchedule/{userId:guid}", (Guid userId)
            => scheduleEndPointOperations.GetUserSchedule(userId));

        scheduleGroupedEndpoint.MapGet("/GetAllUserTerminatedSchedules/{userId:guid}", (Guid userId)
            => scheduleEndPointOperations.GetAllUserTerminatedSchedules(userId));
       
        return app;
    }
}