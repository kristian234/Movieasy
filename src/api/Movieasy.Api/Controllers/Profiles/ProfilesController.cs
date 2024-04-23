using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Profiles.GetProfileById;
using Movieasy.Application.Profiles.UpdateProfile;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Profiles
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ISender _sender;
        public ProfilesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        // It's stored as string in the database because of KeyCloak (and almost all other identity providers) so... not much to do about it
        public async Task<IActionResult> GetProfile(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetProfileByIdQuery(id);

            Result<ProfileResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(
            UpdateProfileRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateProfileCommand(
                request.UserId,
                request.Details);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
