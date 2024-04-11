using ApplicationStructure.DBContext;
using ApplicationStructure.Repositories;
using ApplicationStructure.Services.Operations;
using Domain.Entities;
using Domain.Models.Service;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ServicesProvider.Extensions;

public static class TaskEndpointExtension
{
    public static IEndpointRouteBuilder
        MapTaskEndpoint(this WebApplication app, MeuBarbeiroDbContext dbContext)
    {
        IRepository<ScheduleTask> scheduleRepository = new TaskRepository(dbContext);

        var taskEndPointOperations = new TaskServiceOperations(scheduleRepository);

        var taskGroupedEndpoint = app.MapGroup("/api/Task").WithTags("Task");
        
        taskGroupedEndpoint.MapPost("/AddTask", ([FromBody] TaskModel model)
            => taskEndPointOperations.Add(model));
        
        taskGroupedEndpoint.MapPut("/UpdateTask/{taskId:guid}", 
            (Guid taskId, [FromBody] TaskModel model)
                => taskEndPointOperations.Update(taskId, model));
        
        taskGroupedEndpoint.MapDelete("/DeleteTask/{taskId:guid}", (Guid taskId)
            => taskEndPointOperations.Delete(taskId));

        taskGroupedEndpoint.MapGet("/GetAllTasks", [OutputCache]()
            => taskEndPointOperations.GetAll());
        
        return app;
    }
}