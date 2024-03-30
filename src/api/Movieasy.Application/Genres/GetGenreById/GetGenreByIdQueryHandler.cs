using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Genres.GetGenre;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.GetGenreById
{
    internal sealed class GetGenreByIdQueryHandler : IQueryHandler<GetGenreByIdQuery, GenreResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetGenreByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GenreResponse>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            GenreResponse? genre = await _context
                .Genres
                .Where(g => g.Id == request.GenreId)
                .Select(g => new GenreResponse()
                {
                    Id = g.Id.ToString(),
                    Name = g.Name.Value
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if(genre == null)
            {
                return Result.Failure<GenreResponse>(GenreErrors.NotFound);
            }

            return genre;
        }
    }
}
