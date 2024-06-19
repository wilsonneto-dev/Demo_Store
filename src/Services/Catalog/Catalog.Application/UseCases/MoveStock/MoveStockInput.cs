using MediatR;

namespace Catalog.Application.UseCases.MoveStock;

public record MoveStockInput(
    Guid Id,
    MovementType MovementType,
    int Quantity) : IRequest<MoveStockOutput>;