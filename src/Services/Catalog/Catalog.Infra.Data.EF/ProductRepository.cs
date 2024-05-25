using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Data.EF;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    private DbSet<Product> Products => context.Set<Product>();

    public async Task Insert(Product product, CancellationToken cancellationToken)
    {
        await Products.AddAsync(product, cancellationToken);
    }

    public Task<Product?> Get(Guid id, CancellationToken cancellationToken) =>
        Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<List<Product>> GetAll(CancellationToken cancellationToken) =>
        Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
}