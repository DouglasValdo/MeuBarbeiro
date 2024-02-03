using ApplicationStructure.DBContext;

namespace ApplicationStructure.Repositories;

public class BaseRepository
{
    protected readonly MeuBarbeiroDbContext Context;
    
    protected BaseRepository(MeuBarbeiroDbContext context) => Context = context;
}