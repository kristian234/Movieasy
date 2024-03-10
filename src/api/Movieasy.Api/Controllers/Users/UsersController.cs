using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Users.GetLoggedInUser;
using Movieasy.Application.Users.LoginUser;
using Movieasy.Application.Users.RefreshUser;
using Movieasy.Application.Users.RegisterUser;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            Result<UserResponse> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(
            LoginUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LogInUserCommand(request.Email, request.Password, request.RememberMe);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshUser(
            RefreshUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RefreshUserCommand(request.RefreshToken);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
