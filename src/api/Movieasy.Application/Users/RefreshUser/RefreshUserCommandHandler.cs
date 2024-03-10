using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Users.LoginUser;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Users.RefreshUser
{
    internal sealed class RefreshUserCommandHandler : ICommandHandler<RefreshUserCommand, RefreshTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public RefreshUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<RefreshTokenResponse>> Handle(RefreshUserCommand request, CancellationToken cancellationToken)
        {
            Result<JwtServiceResult> result = await _jwtService.RefreshTokenAsync(
                request.RefreshToken,
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);
            }

            return new RefreshTokenResponse(
                result.Value.AccessToken,
                result.Value.RefreshToken);
        }
    }
}
