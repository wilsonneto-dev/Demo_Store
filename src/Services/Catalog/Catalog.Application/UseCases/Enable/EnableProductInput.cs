using MediatR;

namespace Catalog.Application.UseCases.Enable;

public record EnableProductInput(Guid Id) : IRequest<EnableProductOutput>;