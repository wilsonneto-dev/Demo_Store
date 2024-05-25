using Catalog.Application.Common.Contracts;

namespace Catalog.Infra.Data.EF;

public class UnitOfWork(CatalogDbContext context) : IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);
}