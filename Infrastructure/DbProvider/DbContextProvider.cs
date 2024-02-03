using ApplicationStructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBProvider;

public class DbContextProvider
{
    public MeuBarbeiroDbContext GetDbContext(string connectionString)
    {
        var optBuilder = new DbContextOptionsBuilder<MeuBarbeiroDbContext>()
            .UseSqlServer( connectionString);
        
        return new MeuBarbeiroDbContext(optBuilder.Options);
    }
}