using Microsoft.AspNetCore.SignalR;
using Movieasy.Application.Abstractions.SignalR;
using Movieasy.Application.Movies.GetMovieById;

namespace Movieasy.Infrastructure.SignalR
{
    internal sealed class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewMovieRelease(MovieResponse movieResponse)
        {
            await _hubContext.Clients.Group("MainPage").SendAsync("ReceiveNewMovieRelease", movieResponse);
        }
    }
}
