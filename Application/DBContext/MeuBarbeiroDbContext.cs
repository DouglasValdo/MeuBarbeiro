using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStructure.DBContext;

public class MeuBarbeiroDbContext : DbContext
{
    public MeuBarbeiroDbContext(DbContextOptions options) : base(options){}
    
    public virtual DbSet<Barber> Barbers { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleTask> ScheduleTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
}