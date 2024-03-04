using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
