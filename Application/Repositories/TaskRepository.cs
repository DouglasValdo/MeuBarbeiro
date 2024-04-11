using System.Linq.Expressions;
using ApplicationStructure.DBContext;
using Domain.Entities;
using Domain.Repository;

namespace ApplicationStructure.Repositories;

public class TaskRepository : BaseRepository, IRepository<ScheduleTask>
{
    public TaskRepository(MeuBarbeiroDbContext context) : base(context) { }
    
    public void Add(ScheduleTask toAdd)
    {
        var exist = Context.ScheduleTasks.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.ScheduleTasks.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(ScheduleTask newValue, Expression<Func<ScheduleTask, bool>> expression)
    {
        var task = Context.ScheduleTasks.SingleOrDefault(expression);

        if (task == null) return;

        task.Name       = newValue.Name;
        task.Price      = newValue.Price;
        task.IsAvailable  = newValue.IsAvailable;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<ScheduleTask, bool>> expression)
    {
        var task = Context.ScheduleTasks.SingleOrDefault(expression);

        if (task == null) return;

        task.IsDeleted = true;
        
        Context.SaveChanges();
    }

    public ScheduleTask? Get(Expression<Func<ScheduleTask, bool>> expression)
    {
        var task = Context.ScheduleTasks.SingleOrDefault(expression);

        return task;
    }

    public IList<ScheduleTask> GetAll(Expression<Func<ScheduleTask, bool>> expression)
    {
        return Context.ScheduleTasks.Where(expression).ToList();
    }

    public IList<ScheduleTask> GetAll()
    {
        return Context.ScheduleTasks.ToList();
    }
}