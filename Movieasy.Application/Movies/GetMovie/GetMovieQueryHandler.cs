using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using System.Globalization;

namespace Movieasy.Application.Movies.GetMovie
{
    internal sealed class GetMovieQueryHandler : IQueryHandler<GetMovieQuery, MovieResponse>
    {
        private readonly IMovieRepository _movieRepository;
        public GetMovieQueryHandler(IMovieRepository movieRepositoryk)
        {
            _movieRepository = movieRepositoryk;
        }

        public async Task<Result<MovieResponse>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie == null)
            {
                return Result.Failure<MovieResponse>(MovieErrors.NotFound);
            }

            var movieResponse = new MovieResponse()
            {
                Title = movie.Title.Value,
                Description = movie.Description.Value,
                Duration = movie.Duration.Value,
                Rating = movie.Rating.ToString(),
            };

            return movieResponse;
        }
    }
}
