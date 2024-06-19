using Catalog.Application.Common.Contracts;

namespace Catalog.Data.InMemoryJson;

public class UnitOfWork : IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken) => Task.CompletedTask;
}