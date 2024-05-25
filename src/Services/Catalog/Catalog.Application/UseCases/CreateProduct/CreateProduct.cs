using Catalog.Application.Common.Contracts;
using Catalog.Domain.ProductAggregate;
using MediatR;

namespace Catalog.Application.UseCases.CreateProduct;

public class CreateProduct(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository)
    : IRequestHandler<CreateProductInput, CreateProductOutput>
{
    public async Task<CreateProductOutput> Handle(
        CreateProductInput request, 
        CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price, request.Description);
        await productRepository.Insert(product);
        await unitOfWork.Commit(cancellationToken);
        return CreateProductOutput.From(product);
    }
}