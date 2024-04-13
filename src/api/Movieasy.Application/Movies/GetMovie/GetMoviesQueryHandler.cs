using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using System.Globalization;
using System.Linq.Expressions;

namespace Movieasy.Application.Movies.GetMovie
{
    internal sealed class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, PagedList<MovieResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetMoviesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<MovieResponse>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Movie> moviesQuery = _context.Movies
                .Include(m => m.Photo)
                .Include(m => m.Genres)
                .Include(m => m.Actors);

            // TO DO: In case at any time in the future there is a lot of entries in the database, 
            // this right here isn't really optimised for that, adding some form of an Index will be necessary.
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    EF.Functions.ILike((string)m.Title, $"%{request.SearchTerm}%") ||
                    m.Genres.Any(g => EF.Functions.ILike((string)g.Name, $"%{request.SearchTerm}%")) ||
                    m.Actors.Any(a => EF.Functions.ILike((string)a.Name, $"%{request.SearchTerm}%")));
            }

            Expression<Func<Movie, object>> keySelector = GetSortProperty(request);

            if (request.SortOrder?.ToLower() == "desc")
            {
                moviesQuery = moviesQuery.OrderByDescending(keySelector);
            }
            else
            {
                moviesQuery = moviesQuery.OrderBy(keySelector);
            }

            var movieResponsesQuery = moviesQuery
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
                    TrailerUrl = m.Trailer.Value,
                    ImageUrl = m.Photo.Url.Value,
                })
                .AsNoTracking();

            var movies = await PagedList<MovieResponse>.CreateAsync(
                movieResponsesQuery,
                request.Page,
                request.PageSize);

            return movies;
        }

        private static Expression<Func<Movie, object>> GetSortProperty(GetMoviesQuery request)
        {
            return request.SortColumn switch
            {
                "title" => movie => movie.Title,
                "rating" => movie => movie.Rating,
                "upload" => movie => movie.UploadDate,
                "release" => movie => movie.ReleaseDate ?? DateOnly.MinValue,
                "random" => movie => Guid.NewGuid(),
                _ => movie => movie.Id
            };
        }
    }
}
