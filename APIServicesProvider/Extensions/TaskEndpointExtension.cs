using Application.DBContext;
using Application.Repositories;
using Application.Services.Operations;
using Domain.Repository;
using Domain.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Task = Domain.Entities.Task;

namespace ServicesProvider.Extensions;

public static class TaskEndpointExtension
{
    public static IEndpointRouteBuilder
        MapTaskEndpoint(this WebApplication app, MeuBarbeiroDbContext dbContext)
    {
        IRepository<Task> scheduleRepository = new TaskRepository(dbContext);

        var TaskEndPointOperations = new TaskServiceOperations(scheduleRepository);

        var TaskGroupedEndpoint = app.MapGroup("/api/Task").WithTags("Task");
        
        TaskGroupedEndpoint.MapPost("/AddTask/{model}", ([FromBody] TaskModel model)
            => TaskEndPointOperations.Add(model));
        
        TaskGroupedEndpoint.MapPut("/UpdateTask/{TaskId:guid}/{model}", 
            (Guid TaskId, [FromBody] TaskModel model)
                => TaskEndPointOperations.Update(TaskId, model));
        
        TaskGroupedEndpoint.MapDelete("/DeleteTask/{taskId:guid}", (Guid taskId)
            => TaskEndPointOperations.Delete(taskId));

        TaskGroupedEndpoint.MapGet("/GetAllTasks", [OutputCache]()
            => TaskEndPointOperations.GetAll());
        
        return app;
    }
}