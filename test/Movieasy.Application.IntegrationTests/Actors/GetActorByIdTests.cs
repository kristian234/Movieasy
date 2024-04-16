
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Actors.GetActorById;
using Movieasy.Domain.Actors;
using Movieasy.Infrastructure;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Actors
{
    public class GetActorByIdTests
    {
        private readonly GetActorByIdQuery Query;
        private readonly GetActorByIdQueryHandler _handler;

        private readonly IPublisher _publisherMock;
        private readonly IApplicationDbContext _applicationDbContextMock;

        public GetActorByIdTests()
        {
            _publisherMock = Substitute.For<IPublisher>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _applicationDbContextMock = new ApplicationDbContext(options, _publisherMock);

            Query = new GetActorByIdQuery(Guid.NewGuid());

            _handler = new GetActorByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenActorNotFound()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(ActorErrors.NotFound);
        }
    }
}
