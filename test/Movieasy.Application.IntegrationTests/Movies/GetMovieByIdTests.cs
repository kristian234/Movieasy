using FluentAssertions;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Movies;
using NSubstitute;

namespace Movieasy.Application.UnitTests.Movies
{
    public class GetMovieByIdTests : BaseIntegrationTest
    {
        private readonly GetMovieByIdQuery Query;
        private readonly GetMovieByIdQueryHandler _handler;

        public GetMovieByIdTests()
        {
            Query = new GetMovieByIdQuery(Guid.NewGuid());

            _handler = new GetMovieByIdQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMovieNotFound()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.Error.Should().Be(MovieErrors.NotFound);
        }
    }
}
