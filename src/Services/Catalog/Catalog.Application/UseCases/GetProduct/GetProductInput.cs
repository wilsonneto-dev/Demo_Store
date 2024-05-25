using MediatR;

namespace Catalog.Application.UseCases.GetProduct;

public record GetProductInput(Guid Id) : IRequest<GetProductOutput>;