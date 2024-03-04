﻿using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}
