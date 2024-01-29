using System.Linq.Expressions;
using Application.DBContext;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class ScheduleRepository : BaseRepository, IRepository<Schedule>
{
    public ScheduleRepository(MeuBarbeiroDbContext context) : base(context) { }
    
    public void Add(Schedule toAdd)
    {
        var exist = Context.Schedules.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.Schedules.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(Schedule newValue, Expression<Func<Schedule, bool>> expression)
    {
        var schedule = Context.Schedules.SingleOrDefault(expression);

        if (schedule == null) return;

        schedule.UserId       = newValue.UserId;
        schedule.TimeStamp    = newValue.TimeStamp;
        schedule.Notes        = newValue.Notes;
        schedule.IsTerminated = newValue.IsTerminated;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<Schedule, bool>> expression)
    {
        var schedule = Context.Schedules.SingleOrDefault(expression);

        if (schedule == null) return;

        schedule.IsDeleted = true;
        
        Context.SaveChanges();
    }

    public Schedule? Get(Expression<Func<Schedule, bool>> expression)
    {
        var schedule = Context.Schedules.Include(s => s.Task).SingleOrDefault(expression);

        return schedule;
    }

    public IList<Schedule> GetAll(Expression<Func<Schedule, bool>> expression)
    {
        return Context.Schedules.Include(s => s.Task).Where(expression).ToList();
    }

    public IList<Schedule> GetAll()
    {
        return Context.Schedules.Include(s => s.Task).ToList();
    }
}