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

        var TaskEndPointOperations = new TaskServiceOperations(scheduleRepository);

        var TaskGroupedEndpoint = app.MapGroup("/api/Task").WithTags("Task");
        
        TaskGroupedEndpoint.MapPost("/AddTask", ([FromBody] TaskModel model)
            => TaskEndPointOperations.Add(model));
        
        TaskGroupedEndpoint.MapPut("/UpdateTask/{TaskId:guid}", 
            (Guid TaskId, [FromBody] TaskModel model)
                => TaskEndPointOperations.Update(TaskId, model));
        
        TaskGroupedEndpoint.MapDelete("/DeleteTask/{taskId:guid}", (Guid taskId)
            => TaskEndPointOperations.Delete(taskId));

        TaskGroupedEndpoint.MapGet("/GetAllTasks", [OutputCache]()
            => TaskEndPointOperations.GetAll());
        
        return app;
    }
}