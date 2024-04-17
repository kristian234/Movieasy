using MediatR;
using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Infrastructure;
using NSubstitute;

namespace Movieasy.Application.IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest
    {
        protected readonly IPublisher _publisherMock;
        protected IApplicationDbContext _applicationDbContextMock;

        public BaseIntegrationTest()
        {
            _publisherMock = Substitute.For<IPublisher>();

            var options = BuildDbContextOptions();
            _applicationDbContextMock = new ApplicationDbContext(options, _publisherMock);
        }

        protected DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
        }
    }
}
