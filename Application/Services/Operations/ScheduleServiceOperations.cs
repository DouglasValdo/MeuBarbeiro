using Domain.Common.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Service;
using Domain.Repository;

namespace ApplicationStructure.Services.Operations;

public class ScheduleServiceOperations(IRepository<Schedule> repository)
    : BaseOperations<Schedule>(repository), IScheduleOperations<Schedule>
{
    public OperationOutcome Add(ScheduleModel model)
    {
        try
        {
            var newSchedule = new Schedule
            {
                Id = Guid.NewGuid(),
                IsDeleted = model.IsDeleted,
                UserId = model.UserId,
                Notes = model.Notes,
                ScheduleTaskId = model.TaskId,
                TimeStamp = model.TimeStamp,
                IsTerminated = model.IsTerminated
            };
            
            MyRepository.Add(newSchedule);
            
            return new OperationOutcome
            {
                IsSuccessfully = true
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new OperationOutcome
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome<List<Schedule>>? GetUserSchedule(Guid userId)
    {
        try
        {
            var schedule = MyRepository
                .GetAll((s)
                    => s.UserId == userId && s.IsDeleted == false && 
                       (s.IsTerminated == false)).ToList();
            
            return new OperationOutcome<List<Schedule>> { IsSuccessfully = true, Result = schedule};
        }
        catch (Exception e)
        {
            return new OperationOutcome<List<Schedule>>
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Delete(Guid? scheduleId)
    {
        try
        {
           MyRepository.Remove((s) => s.Id == scheduleId);
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
    
    public OperationOutcome<IList<Schedule>> GetAll()
    {
        try
        {
            return new OperationOutcome<IList<Schedule>>
            {
                IsSuccessfully = true,
                Result = MyRepository.GetAll()
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<Schedule>>
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }

    public OperationOutcome Update(Guid? scheduleId, ScheduleModel model)
    {
        throw new NotImplementedException();
    }

    public OperationOutcome<IList<Schedule>> GetAllUserTerminatedSchedules(Guid userId)
    {
        try
        { 
            var allUserTerminatedSchedules = MyRepository
                .GetAll(s => s.UserId == userId
                             && s.IsTerminated == true
                             && s.IsDeleted == false);
            
            return new OperationOutcome<IList<Schedule>>
            {
                IsSuccessfully = true,
                Result = allUserTerminatedSchedules
            };
        }
        catch (Exception e)
        {
            return new OperationOutcome<IList<Schedule>>
            {
                IsSuccessfully = false,
                ErrorMessage = e.Message
            };
        }
    }
}