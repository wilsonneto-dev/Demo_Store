using Catalog.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Data.EF;

public sealed class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        Database.EnsureCreated();
    }

    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder builder) => 
        builder.ApplyConfiguration(new ProductConfiguration());
}