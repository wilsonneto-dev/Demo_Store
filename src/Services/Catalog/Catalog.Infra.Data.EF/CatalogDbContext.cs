using Catalog.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Data.EF;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder builder) => 
        builder.ApplyConfiguration(new ProductConfiguration());
}