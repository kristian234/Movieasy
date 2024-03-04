using MediatR;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
