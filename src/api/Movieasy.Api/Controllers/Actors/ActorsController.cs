using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Actors.AddActor;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Api.Controllers.Actors
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ISender _sender;
        public ActorsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(
            AddActorRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddActorCommand(
                request.Name,
                request.Biography);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
