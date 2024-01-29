using System.Linq.Expressions;
using Application.DBContext;
using Domain.Repository;
using Task = Domain.Entities.Task;

namespace Application.Repositories;

public class TaskRepository : BaseRepository, IRepository<Task>
{
    public TaskRepository(MeuBarbeiroDbContext context) : base(context) { }
    
    public void Add(Task toAdd)
    {
        var exist = Context.Tasks.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.Tasks.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(Task newValue, Expression<Func<Task, bool>> expression)
    {
        var Task = Context.Tasks.SingleOrDefault(expression);

        if (Task == null) return;

        Task.Name       = newValue.Name;
        Task.Price      = newValue.Price;
        Task.Available  = newValue.Available;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<Task, bool>> expression)
    {
        var Task = Context.Tasks.SingleOrDefault(expression);

        if (Task == null) return;

        Task.IsDeleted = true;
        
        Context.SaveChanges();
    }

    public Task? Get(Expression<Func<Task, bool>> expression)
    {
        var Task = Context.Tasks.SingleOrDefault(expression);

        return Task;
    }

    public IList<Task> GetAll(Expression<Func<Task, bool>> expression)
    {
        return Context.Tasks.Where(expression).ToList();
    }

    public IList<Task> GetAll()
    {
        return Context.Tasks.ToList();
    }
}