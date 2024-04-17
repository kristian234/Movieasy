using FluentAssertions;
using Movieasy.Application.Actors.GetActor;
using Movieasy.Application.Common;
using Movieasy.Application.IntegrationTests.Infrastructure;

namespace Movieasy.Application.IntegrationTests.Actors
{
    public class GetActorsTests : BaseIntegrationTest
    {
        private readonly GetActorsQuery Query;
        private readonly GetActorsQueryHandler _handler;

        public GetActorsTests()
        {
            Query = new GetActorsQuery(GlobalData.SearchTerm, GlobalData.PageNumber, GlobalData.PageSize);

            _handler = new GetActorsQueryHandler(_applicationDbContextMock);
        }

        [Fact]
        public async Task Handle_ShouldReturn_ValidPagedList()
        {
            // Act
            var result = await _handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<PagedList<PagedActorResponse>>();
        }
    }
}
