namespace Catalog.Application.Common.Contracts;

public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}