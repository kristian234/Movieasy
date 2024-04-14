using FluentAssertions;
using Movieasy.Application.Actors.DeleteActor;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Actors
{
    public class DeleteActorTests
    {
        private readonly DeleteActorCommand Command;
        private readonly DeleteActorCommandHandler _handler;

        private readonly IActorRepository _actorRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public DeleteActorTests()
        {
            _actorRepositoryMock = Substitute.For<IActorRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            Command = new DeleteActorCommand(
                Guid.NewGuid());

            _handler = new DeleteActorCommandHandler(
                _actorRepositoryMock,
                _unitOfWorkMock);
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
