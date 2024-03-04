﻿using Microsoft.EntityFrameworkCore;
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
            IQueryable<Movie> moviesQuery = _context.Movies;

            // TO DO: In case at any time in the future there is a lot of entries in the database, 
            // this right here isn't really optimised for that, adding some form of an Index will be necessary.
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    ((string)m.Title).Contains(request.SearchTerm));
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
                    Title = m.Title.Value,
                    Description = m.Description.Value,
                    Duration = m.Duration.Value,
                    Rating = m.Rating.ToString(),
                    ReleaseDate = m.ReleaseDate.HasValue ?
                        m.ReleaseDate.Value.ToString("yyyy/mm/dd", CultureInfo.InvariantCulture)
                        : null,
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
                _ => movie => movie.Id
            };
        }
    }
}