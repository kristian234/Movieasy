using Movieasy.Domain.Users;

namespace Movieasy.Application.Abstractions.Authentication
{
    public interface IAuthenticationService
    {
        public Task<string> RegisterAsync(
            User user,
            string password,
            CancellationToken cancellationToken = default);
    }
}
