using System.Linq.Expressions;
using Application.DBContext;
using Domain.Entities;
using Domain.Repository;

namespace Application.Repositories;

public class UserRepository : BaseRepository, IRepository<User>
{
    public UserRepository(MeuBarbeiroDbContext context) : base(context) { }
    
    public void Add(User toAdd)
    {
        if (toAdd == null) throw new ArgumentException(null, nameof(toAdd));
        
        var exist = Context.Users.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.Users.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(User newValue, Expression<Func<User, bool>> expression)
    {
        if (newValue == null) throw new ArgumentException(null, nameof(newValue));
        
        var userToUpdate = Context.Users.SingleOrDefault(expression);

        if (userToUpdate == null) return;

        userToUpdate.FullName    = newValue.FullName;
        userToUpdate.PhoneNumber = newValue.PhoneNumber;
        userToUpdate.IsDeleted   = newValue.IsDeleted;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<User, bool>> expression)
    {
        var user = Context.Users.SingleOrDefault(expression);

        if (user == null) return;

        user.IsDeleted = true;

        Context.SaveChanges();
    }

    public User? Get(Expression<Func<User, bool>> expression)
    {
        var user = Context.Users.SingleOrDefault(expression);

        return user;
    }

    public IList<User> GetAll(Expression<Func<User, bool>> expression)
    {
        return Context.Users.Where(expression).ToList();
    }

    public IList<User> GetAll()
    {
        return Context.Users.ToList();
    }
}