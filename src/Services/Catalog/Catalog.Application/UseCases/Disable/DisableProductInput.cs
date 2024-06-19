using MediatR;

namespace Catalog.Application.UseCases.Disable;

public record DisableProductInput(Guid Id) : IRequest<DisableProductOutput>;