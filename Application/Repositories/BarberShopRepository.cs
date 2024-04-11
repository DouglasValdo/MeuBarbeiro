using System.Linq.Expressions;
using ApplicationStructure.DBContext;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStructure.Repositories;

public class BarberShopRepository(MeuBarbeiroDbContext context) : BaseRepository(context), IRepository<Barber>
{
    public void Add(Barber toAdd)
    {
        var exist = Context.Barbers.SingleOrDefault(u => u.Id == toAdd.Id);

        if (exist != null) return;

        Context.Barbers.Add(toAdd);

        Context.SaveChanges();
    }

    public void Update(Barber newValue, Expression<Func<Barber, bool>> expression)
    {
        var barberShop = Context.Barbers.SingleOrDefault(expression);

        if (barberShop == null) return;

        barberShop.Name             = newValue.Name;
        barberShop.IsDeleted        = newValue.IsDeleted;
        barberShop.Status           = newValue.Status;
        barberShop.ScheduleStart    = newValue.ScheduleStart;
        barberShop.ScheduleEnd      = newValue.ScheduleEnd;

        Context.SaveChanges();
    }

    public void Remove(Expression<Func<Barber, bool>> expression)
    {
        var barberShop = Context.Barbers.SingleOrDefault(expression);

        if (barberShop == null) return;

        barberShop.IsDeleted = true;
        
        Context.SaveChanges();
    }

    public Barber? Get(Expression<Func<Barber, bool>> expression)
    {
        var barberShop = Context.Barbers
            .Include(b => b.ScheduleTasks)
            .Include(s => s.Schedules)
            .SingleOrDefault(expression);

        return barberShop;
    }

    public IList<Barber> GetAll(Expression<Func<Barber, bool>> expression)
    {
        return Context.Barbers
            .Include(b => b.ScheduleTasks)
            .Include(s => s.Schedules)
            .Where(expression).ToList();
    }

    public IList<Barber> GetAll()
    {
        return Context.Barbers
            .Include(b => b.ScheduleTasks)
            .Include(s => s.Schedules)
            .ToList();
    }
}