using Application.Services.Operations;
using Domain.Repository;
using Domain.Services.Common;
using Domain.Services.Entities;
using Domain.Services.Interfaces;
using Task = Domain.Entities.Task;

namespace Application.Services.Operations;

public class TaskServiceOperations : BaseOperations<Task>, ITaskOperations<Task>
{
    public TaskServiceOperations(IRepository<Task> repository) : base(repository)
    {
    }

    public OperationOutcome Add(TaskModel model)
    {
        try
        {
            var newTaskType = new Task
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                Available = model.Available,
                IsDeleted = model.IsDeleted,
                DurationInMinutes = model.DurationInMinutes
            };

            MyRepository.Add(newTaskType);

            return new OperationOutcome
            {
                IsSucesseful = true
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome
            {
                IsSucesseful = true,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Delete(Guid? taskTypeId)
    {
        try
        {
            MyRepository.Remove((t) => t.Id == taskTypeId);

            return new OperationOutcome { IsSucesseful = true };
        }
        catch (Exception e)
        {
            return new OperationOutcome
            {
                IsSucesseful = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome<IList<Task>> GetAll()
    {
        try
        {
            return new OperationOutcome<IList<Task>>
            {
                IsSucesseful = true,
                Result = MyRepository.GetAll()
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<Task>>
            {
                IsSucesseful = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Update(Guid? taskTypeId, TaskModel model)
    {
        throw new NotImplementedException();
    }
}