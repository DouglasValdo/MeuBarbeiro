using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Service;
using Domain.Repository;

namespace ApplicationStructure.Services.Operations;

public class TaskServiceOperations : BaseOperations<ScheduleTask>, ITaskOperations<ScheduleTask>
{
    public TaskServiceOperations(IRepository<ScheduleTask> repository) : base(repository)
    {
    }

    public OperationOutcome Add(TaskModel model)
    {
        try
        {
            var newTaskType = new ScheduleTask
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                IsAvailable = model.Available,
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

    public OperationOutcome<IList<ScheduleTask>> GetAll()
    {
        try
        {
            return new OperationOutcome<IList<ScheduleTask>>
            {
                IsSucesseful = true,
                Result = MyRepository.GetAll()
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<ScheduleTask>>
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