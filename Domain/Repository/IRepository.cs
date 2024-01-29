using System.Linq.Expressions;

namespace Domain.Repository;

public interface IRepository<T> where T: class
{
    public void Add(T toAdd);
    public void Update(T newValue, Expression<Func<T, bool>> expression);
    public void Remove(Expression<Func<T, bool>> expression);
    public T? Get(Expression<Func<T, bool>> expression);
    public IList<T> GetAll(Expression<Func<T, bool>> expression);
    public IList<T> GetAll();
}