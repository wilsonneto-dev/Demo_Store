using Catalog.Application.Common.Contracts;
using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Catalog.Application.UseCases.CreateProduct;

public class CreateProduct(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    ILogger<CreateProduct> logger)
    : IRequestHandler<CreateProductInput, CreateProductOutput>
{
    public async Task<CreateProductOutput> Handle(
        CreateProductInput request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new product. {@Request}", request);

        var product = new Product(request.Name, request.Price, request.Description);
        await productRepository.Insert(product, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        logger.LogInformation("Product {Id} created. {@Product}", product.Id, product);
        return CreateProductOutput.From(product);
    }
}