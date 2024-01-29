using Domain.Repository;

namespace Application.Services.Operations;

public class BaseOperations<T> where T: class
{
    protected readonly IRepository<T> MyRepository;

    protected BaseOperations(IRepository<T> repository) => MyRepository = repository;
}