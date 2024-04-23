using FluentAssertions;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Application.Profiles.GetProfileById;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Users;

namespace Movieasy.Application.IntegrationTests.Profiles
{
    public class GetProfileByIdTests : BaseIntegrationTest
    {
        private readonly GetProfileByIdQuery Query;
        private readonly GetProfileByIdQueryHandler _handler;

        public GetProfileByIdTests()
        {
            Query = new GetProfileByIdQuery(Guid.NewGuid());

            _handler = new GetProfileByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(UserErrors.NotFound);
        }
    }
}
