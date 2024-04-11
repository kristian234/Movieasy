using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Actors.GetActorById;
using Movieasy.Application.Movies.GetMovieActorsQuery;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Actors.GetMovieActorsQueryHandler
{
    internal sealed class GetMovieActorsQueryHandler : IQueryHandler<GetMovieActorsQuery, IEnumerable<ActorResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetMovieActorsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ActorResponse>>> Handle(GetMovieActorsQuery request, CancellationToken cancellationToken)
        {
            var movieExists = await _context.Movies.AnyAsync(movie => movie.Id == request.MovieId, cancellationToken);
            if (!movieExists)
            {
                return Result.Failure<IEnumerable<ActorResponse>>(MovieErrors.NotFound);
            }

            var actors = await _context.Movies
                .Include(m => m.Actors)
                .Where(m => m.Id == request.MovieId)
                .SelectMany(m => m.Actors)
                .Select(a => new ActorResponse()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name.Value,
                    Biography = a.Biography.Value,
                })
                .ToListAsync(cancellationToken);

            return actors;
        }
    }
}
