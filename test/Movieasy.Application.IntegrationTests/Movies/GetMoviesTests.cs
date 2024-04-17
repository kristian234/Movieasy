using FluentAssertions;
using Movieasy.Application.Actors.GetActor;
using Movieasy.Application.Common;
using Movieasy.Application.IntegrationTests.Infrastructure;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Movies.GetMovieById;

namespace Movieasy.Application.IntegrationTests.Movies
{
    public class GetMoviesTests : BaseIntegrationTest
    {
        private readonly GetMoviesQuery Query;
        private readonly GetMoviesQueryHandler _handler;

        public GetMoviesTests()
        {
            Query = new GetMoviesQuery(GlobalData.SearchTerm, GlobalData.SortColumn, GlobalData.SortOrder, GlobalData.PageNumber, GlobalData.PageSize);

            _handler = new GetMoviesQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_ValidPagedList()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<PagedList<MovieResponse>>();
        }
    }
}
