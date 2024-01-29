using Application.DBContext;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.DBProvider;

namespace MeuBarbeiroTests;

public class Tests
{
    private MeuBarbeiroDbContext _dbContext;
    [SetUp]
    public void Setup()
    {
        var connectionString =
            "Data Source=SQL6031.site4now.net;Initial Catalog=db_a9fe05_nhabarberu;" +
            "User Id=db_a9fe05_nhabarberu_admin;Password=D0uglas$1020;TrustServerCertificate=True";
        
        _dbContext = new DbContextProvider().GetDbContext(connectionString);
    }

    [Test]
    public void get_exception_add_invalid_user()
    {
        var userRepository = new UserRepository(_dbContext);

        Assert.Throws<ArgumentException>(() => { userRepository.Add(null);});
    }

    [Test]
    public void get_valid_hardcoded_user()
    {
        var userRepository = new UserRepository(_dbContext);
        var hardCodedUser = userRepository.Get(user => user.PhoneNumber == 936101658);
        
        Assert.That(hardCodedUser, Is.Not.Null);
        Assert.That(hardCodedUser, Is.InstanceOf(typeof(User)));
    }
}