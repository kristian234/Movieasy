using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews.Event
{
    public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}
