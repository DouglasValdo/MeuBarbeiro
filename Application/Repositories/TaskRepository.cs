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
        var exist = Context.ScheduleTask.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.ScheduleTask.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(ScheduleTask newValue, Expression<Func<ScheduleTask, bool>> expression)
    {
        var Task = Context.ScheduleTask.SingleOrDefault(expression);

        if (Task == null) return;

        Task.Name       = newValue.Name;
        Task.Price      = newValue.Price;
        Task.IsAvailable  = newValue.IsAvailable;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<ScheduleTask, bool>> expression)
    {
        var Task = Context.ScheduleTask.SingleOrDefault(expression);

        if (Task == null) return;

        Task.IsDeleted = true;
        
        Context.SaveChanges();
    }

    public ScheduleTask? Get(Expression<Func<ScheduleTask, bool>> expression)
    {
        var Task = Context.ScheduleTask.SingleOrDefault(expression);

        return Task;
    }

    public IList<ScheduleTask> GetAll(Expression<Func<ScheduleTask, bool>> expression)
    {
        return Context.ScheduleTask.Where(expression).ToList();
    }

    public IList<ScheduleTask> GetAll()
    {
        return Context.ScheduleTask.ToList();
    }
}