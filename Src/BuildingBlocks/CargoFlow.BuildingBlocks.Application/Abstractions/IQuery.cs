using ErrorOr;
using MediatR;

namespace CargoFlow.BuildingBlocks.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{

}
