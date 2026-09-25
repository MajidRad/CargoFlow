using ErrorOr;
using MediatR;

namespace CargoFlow.BuildingBlocks.Application.Abstractions;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
