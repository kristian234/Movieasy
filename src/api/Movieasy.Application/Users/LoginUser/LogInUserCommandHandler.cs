using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Users.LoginUser
{
    internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public LogInUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LogInUserCommand request,
            CancellationToken cancellationToken)
        {
            Result<JwtServiceResult> result = await _jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return new AccessTokenResponse(
                result.Value.AccessToken,
                request.RememberMe ? result.Value.RefreshToken : null);
        }
    }
}
