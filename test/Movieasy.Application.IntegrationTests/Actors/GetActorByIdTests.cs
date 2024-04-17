
using FluentAssertions;
using Movieasy.Application.Actors.GetActorById;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.IntegrationTests.Actors
{
    public class GetActorByIdTests : BaseIntegrationTest
    {
        private readonly GetActorByIdQuery Query;
        private readonly GetActorByIdQueryHandler _handler;

        public GetActorByIdTests()
        {
            Query = new GetActorByIdQuery(Guid.NewGuid());

            _handler = new GetActorByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenActorNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(ActorErrors.NotFound);
        }
    }
}
