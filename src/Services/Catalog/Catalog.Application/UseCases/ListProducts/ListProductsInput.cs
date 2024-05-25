using MediatR;

namespace Catalog.Application.UseCases.ListProducts;

public record ListProductsInput : IRequest<ListProductsOutput>;