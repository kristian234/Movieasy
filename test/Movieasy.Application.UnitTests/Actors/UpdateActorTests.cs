using FluentAssertions;
using Movieasy.Application.Actors.UpdateActor;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Actors
{
    public class UpdateActorTests
    {
        private readonly UpdateActorCommand Command;
        private readonly UpdateActorCommandHandler _handler;

        private readonly IActorRepository _actorRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkRepositoryMock;

        public UpdateActorTests()
        {
            _actorRepositoryMock = Substitute.For<IActorRepository>();
            _unitOfWorkRepositoryMock = Substitute.For<IUnitOfWork>();

            Command = new UpdateActorCommand(
                Guid.NewGuid(),
                ActorData.Name,
                ActorData.Biography);

            _handler = new UpdateActorCommandHandler(
                _actorRepositoryMock,
                _unitOfWorkRepositoryMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenActorNotFound()
        {
            // Arrange
            _actorRepositoryMock
                .GetByIdAsync(Command.ActorId, Arg.Any<CancellationToken>())
                .Returns((Actor?)null);

            // Act
            var result = await _handler.Handle(Command, default);

            // Assert
            result.Error.Should().Be(ActorErrors.NotFound);
        }
    }
}
