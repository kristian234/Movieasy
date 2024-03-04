﻿using Microsoft.EntityFrameworkCore;
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
                .Select(m => new MovieResponse
                {
                    Title = m.Title.Value,
                    Description = m.Description.Value,
                    Duration = m.Duration.Value,
                    Rating = m.Rating.ToString(),
                    ReleaseDate = m.ReleaseDate.HasValue ?
                        m.ReleaseDate.Value
                         .ToString(CultureInfo.InvariantCulture)
                        : null

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