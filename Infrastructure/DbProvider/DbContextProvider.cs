using ApplicationStructure.DBContext;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBProvider;

public class DbContextProvider : IDisposable
{
    private MeuBarbeiroDbContext? _dbContext;

    public MeuBarbeiroDbContext GetDbContext(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new NoConnectionStringProvidedException();

        var optBuilder = new DbContextOptionsBuilder<MeuBarbeiroDbContext>()
            .UseSqlServer(connectionString);

        _dbContext = new MeuBarbeiroDbContext(optBuilder.Options);

        return _dbContext;
    }
    public void Dispose() => GC.SuppressFinalize(this);
}