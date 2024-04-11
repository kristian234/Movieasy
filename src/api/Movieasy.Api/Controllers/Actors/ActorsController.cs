using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieasy.Application.Actors.AddActor;
using Movieasy.Application.Actors.DeleteActor;
using Movieasy.Application.Actors.GetActor;
using Movieasy.Application.Actors.GetActorById;
using Movieasy.Application.Actors.UpdateActor;
using Movieasy.Application.Movies.GetMovie;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetActorByIdQuery(id);

            Result<ActorResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetActors(
            string? searchTerm,
            CancellationToken cancellationToken,
            int pageNumber = 1,
            int pageSize = 4)
        {
            var query = new GetActorsQuery(searchTerm, pageNumber, pageSize);

            Result<PagedList<PagedActorResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
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

        [HttpPut]
        public async Task<IActionResult> UpdateActor(
            UpdateActorRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateActorCommand(
                request.ActorId,
                request.Name,
                request.Biography);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteActorCommand(id);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
