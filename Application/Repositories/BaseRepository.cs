using Application.DBContext;

namespace Application.Repositories;

public class BaseRepository
{
    protected readonly MeuBarbeiroDbContext Context;
    
    protected BaseRepository(MeuBarbeiroDbContext context) => Context = context;
}