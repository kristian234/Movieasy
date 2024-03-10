using Movieasy.Application.Users.LoginUser;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        Task<Result<JwtServiceResult>> GetAccessTokenAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default);

        Task<Result<JwtServiceResult>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default);
    }
}
