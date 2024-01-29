using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Application.DBContext;

public class MeuBarbeiroDbContext : DbContext
{
    public MeuBarbeiroDbContext(DbContextOptions options):base(options){}
    
    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }
}