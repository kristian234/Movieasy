using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using System.Globalization;

namespace Movieasy.Application.Movies.GetMovieById
{
    internal sealed class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetMovieByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<MovieResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            MovieResponse? movie = await _context
                .Movies
                .Where(m => m.Id == request.MovieId)
                .Include(m => m.Photo)
                .Include(m => m.Genres)
                .Select(m => new MovieResponse
                {
                    Id = m.Id.ToString(),
                    Title = m.Title.Value,
                    Description = m.Description.Value,
                    Duration = m.Duration.Value,
                    Rating = m.Rating.ToString(),
                    ReleaseDate = m.ReleaseDate.HasValue ?
                        m.ReleaseDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        : null,
                    UploadDate = m.UploadDate.ToString("f", CultureInfo.InvariantCulture),
                    ImageUrl = m.Photo.Url.Value,
                    TrailerUrl = m.Trailer.Value,
                    Genres = m.Genres.Select(genre => new Genres.GetGenre.GenreResponse()
                    {
                        Id = genre.Id.ToString(),
                        Name = genre.Name.Value,
                    })               
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (movie == null)
            {
                return Result.Failure<MovieResponse>(MovieErrors.NotFound);
            }

            return movie;
        }
    }
}
