using MediatR;

namespace Catalog.Application.UseCases.CreateProduct;

public record CreateProductInput(
    string Name,
    string Description,
    decimal Price) : IRequest<CreateProductOutput>;