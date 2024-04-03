using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Movieasy.Infrastructure.SignalR
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }
        private const string groupName = "MainPage";

        public async Task JoinMainPageGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveMainPageGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
