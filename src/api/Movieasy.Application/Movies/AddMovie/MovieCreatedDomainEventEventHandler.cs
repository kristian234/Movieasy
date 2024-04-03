using MediatR;
using Microsoft.AspNetCore.SignalR;
using Movieasy.Application.Abstractions.SignalR;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Movies.Events;
using System.Globalization;

namespace Movieasy.Application.Movies.AddMovie
{
    internal sealed class MovieCreatedDomainEventEventHandler : INotificationHandler<MovieCreatedDomainEvent>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly INotificationService _notificationService;
        public MovieCreatedDomainEventEventHandler(
            IMovieRepository movieRepository,
            INotificationService notificationService)
        {
            _movieRepository = movieRepository;
            _notificationService = notificationService; 
        }

        public async Task Handle(MovieCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(notification.MovieId);

            if(movie == null)
            {
                return;
            }

            MovieResponse response = new MovieResponse()
            {
                Id = movie.Id.ToString(),
                Title = movie.Title.Value,
                Description = movie.Description.Value,
                Duration = movie.Duration.Value,
                Rating = movie.Rating.ToString(),
                ReleaseDate = movie.ReleaseDate.HasValue ?
                        movie.ReleaseDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        : null,
                UploadDate = movie.UploadDate.ToString("f", CultureInfo.InvariantCulture),
                ImageUrl = movie.Photo.Url.Value,
            };

            await _notificationService.NotifyNewMovieRelease(response);
        }
    }
}
