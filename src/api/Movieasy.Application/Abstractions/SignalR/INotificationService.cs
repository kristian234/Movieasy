using Movieasy.Application.Movies.GetMovieById;

namespace Movieasy.Application.Abstractions.SignalR
{
    public interface INotificationService
    {
        public Task NotifyNewMovieRelease(MovieResponse movie);
    }
}
