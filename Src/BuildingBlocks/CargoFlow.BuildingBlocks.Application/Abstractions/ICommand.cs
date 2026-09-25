using ErrorOr;
using MediatR;

namespace CargoFlow.BuildingBlocks.Application.Abstractions;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{

}
