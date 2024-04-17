using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Common;

namespace Movieasy.Application.Actors.GetActor
{
    public sealed record GetActorsQuery(
        string? SearchTerm,
        int PageNumber,
        int PageSize) : IQuery<PagedList<PagedActorResponse>>; 
}
