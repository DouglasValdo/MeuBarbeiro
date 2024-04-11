using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Service;
using Domain.Repository;

namespace ApplicationStructure.Services.Operations;

public class TaskServiceOperations(IRepository<ScheduleTask> repository)
    : BaseOperations<ScheduleTask>(repository), ITaskOperations<ScheduleTask>
{
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
                IsSuccessfully = true
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome
            {
                IsSuccessfully = true,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Delete(Guid? taskTypeId)
    {
        try
        {
            MyRepository.Remove((t) => t.Id == taskTypeId);

            return new OperationOutcome { IsSuccessfully = true };
        }
        catch (Exception e)
        {
            return new OperationOutcome
            {
                IsSuccessfully = false,
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
                IsSuccessfully = true,
                Result = MyRepository.GetAll()
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<ScheduleTask>>
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Update(Guid? taskTypeId, TaskModel model)
    {
        throw new NotImplementedException();
    }
}