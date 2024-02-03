﻿using Domain.Common.Service;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IScheduleService
{
    public Task<OperationOutcome<Schedule?>?> GetUserScheduleAsync(Guid userId);
    public Task<OperationOutcome?> DeleteScheduleAsync(Guid scheduleId);
    public Task<OperationOutcome<IList<Schedule?>>?> GetUserTerminatedSchedulesAsync(Guid userId);
}