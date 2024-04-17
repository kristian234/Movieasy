using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Common;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.GetActor
{
    internal sealed class GetActorsQueryHandler : IQueryHandler<GetActorsQuery, PagedList<PagedActorResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetActorsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<PagedActorResponse>>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Actor> actorsQuery = _context.Actors;

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                actorsQuery = actorsQuery.Where(a =>
                    ((string)a.Name).Contains(request.SearchTerm));
            }

            var actorsResponseQuery = actorsQuery
                .Select(a => new PagedActorResponse()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name.Value
                })
                .AsNoTracking();

            var actors = await PagedList<PagedActorResponse>.CreateAsync(
                actorsResponseQuery,
                request.PageNumber,
                request.PageSize);

            return actors;
        }
    }
}
