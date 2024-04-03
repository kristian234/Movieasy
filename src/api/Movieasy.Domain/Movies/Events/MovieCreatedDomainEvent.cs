using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies.Events
{
    public sealed record MovieCreatedDomainEvent(Guid MovieId) : IDomainEvent;
}
